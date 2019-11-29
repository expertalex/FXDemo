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

            // TODO: Refactor.... 
            var match = await _context.Match.Where(m => m.Id == id).
                Select(s => new MatchResponse
                {
                    Name = s.Name,
                    HouseTeamManager = s.HouseTeamManager,
                    AwayTeamManager = s.AwayTeamManager,
                    Referee = s.Referee,
                    Date = s.Date,
                    HouseTeamPlayers = s.HouseTeamPlayers.Select(o => o.Player).ToList(),
                    AwayTeamPlayers = s.AwayTeamPlayers.Select(o => o.Player).ToList(),
                }
            ).FirstOrDefaultAsync();

            /*
            .Include(ma => ma.AwayTeamManager)
            .Include(mh => mh.HouseTeamManager)
            .Include(r => r.Referee)
            .Include(ht => ht.HouseTeamPlayers).ThenInclude(htp => htp.Player) // Cuse Self Reinforsing Loop
            .Include(ap => ap.AwayTeamPlayers).ThenInclude(app => app.Player) // Cuse Self Reinforsing Loop
            .FirstOrDefaultAsync(m => m.Id == id);
            */
            if (match == null)
            {
                return null;
            }

            return match;

        }

        public async Task<IEnumerable<MatchResponse>> GetMatcheAsync()
        {
            // TODO: Refactor.... (Manual mpping is not good.... )
            var match = await _context.Match.
                Select(s => new MatchResponse
                {
                    Name = s.Name,
                    HouseTeamManager = s.HouseTeamManager,
                    AwayTeamManager = s.AwayTeamManager,
                    Referee = s.Referee,
                    Date = s.Date,
                    HouseTeamPlayers = s.HouseTeamPlayers.Select(o => o.Player).ToList(),
                    AwayTeamPlayers = s.AwayTeamPlayers.Select(o => o.Player).ToList(),
                }
            ).ToListAsync();

            return match;

        }

        public async Task<MatchResponse> AddMatchAsync(MatchRequest match)
        {


            // TODO:
            // 2. Test
            // 3. Refacture

            // Important: Not eficient, refactoring needed
            ICollection<int> housePlayersId = match.HouseTeam.Where(c => c > 0).ToList();
            ICollection<int> awayPlayersId = match.AwayTeam.Where(c => c > 0).ToList();
            var houseManagerId = match.HouseTeam.Where(c => c < 0).FirstOrDefault(); // By convention, mnagers have negative id (TODO: Refacotr)
            var awayManagerId = match.AwayTeam.Where(c => c < 0).FirstOrDefault(); // By convention, mnagers have negative id (TODO: Refacotr)

            houseManagerId = Math.Abs(houseManagerId);
            awayManagerId = Math.Abs(awayManagerId);
            // Validate
            if (houseManagerId == awayManagerId)
            {
                return null;
            }
            if (housePlayersId.Count() != 11 || awayPlayersId.Count() != 11)
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
            //using (var transaction = _context.Database.BeginTransaction())
            //{

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
                    //transaction.Commit();

                }
                catch (Exception)
                {
                    // TODO: Handle failure
                    return null;
                }
            //}
            var mapper = _mappingConfiguration.CreateMapper();
            //return mapper.Map<MatchResponse>(_match);
            // TODO: Map each child porpertyes to Responce
            return new MatchResponse
            {
                Name = _match.Name,
                HouseTeamManager = _match.HouseTeamManager,
                AwayTeamManager = _match.AwayTeamManager,
                Referee = _match.Referee,
                Date = _match.Date,
                HouseTeamPlayers = _match.HouseTeamPlayers.Select(o => o.Player).ToList(),
                AwayTeamPlayers = _match.AwayTeamPlayers.Select(o => o.Player).ToList(),
            };

        }

        public async Task<MatchResponse> FindMatchAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<MatchResponse> RemoveMatchAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<MatchResponse> UpdateMatchAsync(MatchRequest match, int id)
        {
            /*
             *             var p = _context.Player.Update(mapper.Map<Player>(player));
            await _context.SaveChangesAsync();
            */
            throw new NotImplementedException();
        }

        // TODO: Remove
        public FXDataContext getContext()
        {
            return this._context;
        }

    }
}
