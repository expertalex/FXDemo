using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
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
            // TODO: Refactor.... 
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



            var housePlayers = match.HouseTeam;
            var awayPlayers = match.AwayTeam;
            var houseManager = 0;
            var awayManager = 0;



            var _match = new Match(

            );

            await _context.Match.AddAsync(_match);
            await _context.SaveChangesAsync();


            foreach (int player in housePlayers)
            {
                var matchInDataBase =_context.MatchPlayersHouse.Where(
                            s =>
                            s.Match.Id == _match.Id &&
                            s.Player.Id == player
                ).SingleOrDefault();

                if (matchInDataBase == null)
                {
                    _context.MatchPlayersHouse.Add(new MatchPlayersHouse { MatchId = _match.Id, PlayerId = player });
                }
            }

            foreach (int player in awayPlayers)
            {
                var matchInDataBase = _context.MatchPlayersAway.Where(
                            s =>
                            s.Match.Id == _match.Id &&
                            s.Player.Id == player
                ).SingleOrDefault();

                if (matchInDataBase == null)
                {
                    _context.MatchPlayersAway.Add(new MatchPlayersAway { MatchId = _match.Id, PlayerId = player });
                }
            }

            await _context.SaveChangesAsync();


            return null;

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

        public FXDataContext getContext()
        {
            return this._context;
        }

    }
}
