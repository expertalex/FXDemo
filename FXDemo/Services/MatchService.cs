using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FXDemo.Contracts;
using FXDemo.Data;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FXDemo.Models;
using FXDemo.Models.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FXDemo.Services
{
    public class MatchService : IMatchService
    {
        public const int PlayersPerMatch = 11; // 11 Players per match and Team

        private readonly FXDataContext _context;

        // TODO: Can i make automaper work??? 
        private readonly AutoMapper.IConfigurationProvider _mappingConfiguration;

        public MatchService(
            FXDataContext context, AutoMapper.IConfigurationProvider mappingConfiguration)
        {
            _context = context;
            _mappingConfiguration = mappingConfiguration;
        }

        public async Task<MatchResponse> GetMatcheAsync(int id)
        {

            var match = await _context.Match
                .Include(m => m.AwayTeamManager)
                .Include(m => m.HouseTeamManager)
                .Include(m => m.Referee)
                .Include(m => m.HouseTeamPlayers).ThenInclude(m => m.Player)
                .Include(m => m.AwayTeamPlayers).ThenInclude(m => m.Player)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (match == null)
            {
                return null;
            }
            var mapper = _mappingConfiguration.CreateMapper();
            return mapper.Map<MatchResponse>(match);

        }

        public async Task<IEnumerable<MatchResponse>> GetMatcheAsync()
        {

            var query = _context.Match.ProjectTo<MatchResponse>(_mappingConfiguration);

            var responce = await query.ToListAsync();

            return responce;

        }

        public async Task<MatchResponse> AddMatchAsync(MatchRequest match)
        {


            // TODO:
            // 2. Test
            // 3. Refacture
            ICollection<int> housePlayersId = match.HouseTeam.Where(c => c > 0).ToHashSet(); // To ensure unique players .ToHashSet();
            ICollection<int> awayPlayersId = match.AwayTeam.Where(c => c > 0).ToHashSet(); // .ToHashSet();
            var houseManagerId = match.HouseTeam.Where(c => c < 0).FirstOrDefault(); // By convention, mnagers have negative id (TODO: Refacotr)
            var awayManagerId = match.AwayTeam.Where(c => c < 0).FirstOrDefault(); // By convention, mnagers have negative id (TODO: Refacotr)

            houseManagerId = Math.Abs(houseManagerId);
            awayManagerId = Math.Abs(awayManagerId);
            // Validate
            if (houseManagerId == awayManagerId)
            {
                return null;
            }
            if (housePlayersId.Count() != PlayersPerMatch || awayPlayersId.Count() != PlayersPerMatch)
            {
                return null;
            }

            var houseManager = await _context.Manager.FindAsync(houseManagerId);
            if(houseManager == null)
            {
                return null;
            }
            var awayManager = await _context.Manager.FindAsync(awayManagerId);
            if (houseManager == null)
            {
                return null;
            }
            var referee = await _context.Referee.FindAsync(match.Referee);
            if (referee == null)
            {
                return null;
            }

            var _match = new Match
            {
                Name = match.Name,
                HouseTeamManagerId = houseManagerId,
                AwayTeamManagerId = awayManagerId,
                RefereeId = match.Referee,
                Date = match.Date,
            };

            // Cimented for in memeory DB 
            using (var transaction = _context.Database.BeginTransaction())
            {

                try
                {

                    await _context.Match.AddAsync(_match);
                    await _context.SaveChangesAsync();

                    foreach (int playerId in housePlayersId)
                    {
                        var player = await _context.Player.FindAsync(playerId);
                        if (player == null)
                        {
                            throw new ArgumentException("Player not found");
                        }
                        if (player.TeamName != houseManager.TeamName)
                        {
                            throw new ArgumentException("Team not Match");
                        }

                        var matchInDataBase = _context.MatchPlayersHouse.Where(
                                    s =>
                                    s.Match.Id == _match.Id &&
                                    s.Player.Id == playerId
                        ).SingleOrDefault();

                        if (matchInDataBase == null)
                        {
                            await _context.MatchPlayersHouse.AddAsync(new MatchPlayersHouse { MatchId = _match.Id, PlayerId = playerId });
                        }
                    }

                    foreach (int playerId in awayPlayersId)
                    {
                        var player = await _context.Player.FindAsync(playerId);
                        if (player == null)
                        {
                            throw new ArgumentException("Player not found");
                        }
                        if(player.TeamName != awayManager.TeamName)
                        {
                          throw new ArgumentException("Team not Match");
                        }

                        var matchInDataBase = _context.MatchPlayersAway.Where(
                                    s =>
                                    s.Match.Id == _match.Id &&
                                    s.Player.Id == playerId
                        ).SingleOrDefault();

                        if (matchInDataBase == null)
                        {
                            await _context.MatchPlayersAway.AddAsync(new MatchPlayersAway { MatchId = _match.Id, PlayerId = playerId });
                        }
                    }

                    await _context.SaveChangesAsync();
                    // Commit transaction if all commands succeed, transaction will auto-rollback
                    // when disposed if either commands fails
                    transaction.Commit();

                }
                catch (Exception)
                {
                    // TODO: Handle failure
                    return null;
                }
            }
            var mapper = _mappingConfiguration.CreateMapper();
            return mapper.Map<MatchResponse>(_match);
           

        }

        public async Task<MatchResponse> FindMatchAsync(int id)
        {
            var match = await _context.Match.FindAsync(id);

            var mapper = _mappingConfiguration.CreateMapper();
            return mapper.Map<MatchResponse>(match);
        }

        public async Task<MatchResponse> RemoveMatchAsync(int id)
        {
            var match = await _context.Match.FindAsync(id);
            if (match == null)
            {
                return null;
            }

            _context.Match.Remove(match);
            await _context.SaveChangesAsync();

            var mapper = _mappingConfiguration.CreateMapper();
            return mapper.Map<MatchResponse>(match);
        }

        public async Task<MatchResponse> UpdateMatchAsync(MatchRequest match, int id)
        {
            // TODO:
            // 2. Test
            // 3. Refacture

            var existingMatch = await _context.Match.FindAsync(id);
            if(existingMatch== null)
            {
                return null;
            }

            // Important: Not eficient, refactoring needed (Shood be uinique? Maybe use Sets insted!)
            ICollection<int> housePlayersId = match.HouseTeam.Where(c => c > 0).ToHashSet();
            ICollection<int> awayPlayersId = match.AwayTeam.Where(c => c > 0).ToHashSet();
            var houseManagerId = match.HouseTeam.Where(c => c < 0).FirstOrDefault(); // By convention, mnagers have negative id (TODO: Refacotr)
            var awayManagerId = match.AwayTeam.Where(c => c < 0).FirstOrDefault(); // By convention, mnagers have negative id (TODO: Refacotr)

            // Important to avoid sign reference
            houseManagerId = Math.Abs(houseManagerId);
            awayManagerId = Math.Abs(awayManagerId);

            // Validate
            if (houseManagerId == awayManagerId)
            {
                return null;
            }
            if (housePlayersId.Count() != PlayersPerMatch || awayPlayersId.Count() != PlayersPerMatch)
            {
                return null;
            }
            var houseManager = await _context.Manager.FindAsync(houseManagerId);
            if (houseManager == null)
            {
                return null;
            }
            var awayManager = await _context.Manager.FindAsync(awayManagerId);
            if (houseManager == null)
            {
                return null;
            }
            var referee = await _context.Referee.FindAsync(match.Referee);
            if (referee == null)
            {
                return null;
            }

            existingMatch.Name = match.Name;
            existingMatch.HouseTeamManagerId = houseManagerId;
            existingMatch.AwayTeamManagerId = awayManagerId;
            existingMatch.RefereeId = match.Referee;
            existingMatch.Date = match.Date;
            existingMatch.HouseTeamPlayers = new List<MatchPlayersHouse>();
            existingMatch.AwayTeamPlayers = new List<MatchPlayersAway>();

            // Cimented for in memeory DB 
            using (var transaction = _context.Database.BeginTransaction())
            {

                try
                {
                    _context.Match.Update(existingMatch);

                    // Hack
                    // Remove table before insert new...
                    // TODO: Add relationship to ensure automatic update
                    _context.MatchPlayersHouse.RemoveRange(_context.MatchPlayersHouse.Where(m => m.MatchId == existingMatch.Id));
                    _context.MatchPlayersAway.RemoveRange(_context.MatchPlayersAway.Where(m => m.MatchId == existingMatch.Id));
                    await _context.SaveChangesAsync();

                    foreach (int playerId in housePlayersId)
                    {
                        var player = await _context.Player.FindAsync(playerId);
                        if (player == null)
                        {
                            throw new ArgumentException("Player not found");
                        }
                        if (player.TeamName != houseManager.TeamName)
                        {
                            throw new ArgumentException("Team not Match");
                        }

                        var matchInDataBase = _context.MatchPlayersHouse.Where(
                                    s =>
                                    s.Match.Id == existingMatch.Id &&
                                    s.Player.Id == playerId
                        ).SingleOrDefault();

                        if (matchInDataBase == null)
                        {
                            _context.MatchPlayersHouse.Update(new MatchPlayersHouse { MatchId = existingMatch.Id, PlayerId = playerId });
                        }
                    }

                    foreach (int playerId in awayPlayersId)
                    {
                        var player = await _context.Player.FindAsync(playerId);
                        if (player == null)
                        {
                            throw new ArgumentException("Player not found");
                        }
                        if (player.TeamName != awayManager.TeamName)
                        {
                            throw new ArgumentException("Team not Match");
                        }

                        var matchInDataBase = _context.MatchPlayersAway.Where(
                                    s =>
                                    s.Match.Id == existingMatch.Id &&
                                    s.Player.Id == playerId
                        ).SingleOrDefault();

                        if (matchInDataBase == null)
                        {
                            _context.MatchPlayersAway.Update(new MatchPlayersAway { MatchId = existingMatch.Id, PlayerId = playerId });
                        }
                    }

                    await _context.SaveChangesAsync();
                    // Commit transaction if all commands succeed, transaction will auto-rollback
                    // when disposed if either commands fails
                    transaction.Commit();

                }
                catch (Exception)
                {
                    // TODO: Handle failure
                    return null;
                }
            }

            var mapper = _mappingConfiguration.CreateMapper();
            return mapper.Map<MatchResponse>(existingMatch);
        }

        // TODO: Remove
        public FXDataContext getContext()
        {
            return this._context;
        }

    }
}
