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
    public class PlayerService : IPlayerService
    {

        private readonly FXDataContext _context;
        private readonly AutoMapper.IConfigurationProvider _mappingConfiguration;

        public PlayerService(
            FXDataContext context, AutoMapper.IConfigurationProvider mappingConfiguration)
        {
            _context = context;
            _mappingConfiguration = mappingConfiguration;
        }

        public async Task<IEnumerable<PlayerResponse>> GetPlayersAsync()
        {

            var query = _context.Player.ProjectTo<PlayerResponse>(_mappingConfiguration);

            var responce = await query.ToListAsync();

            return responce;
        }


        public async Task<PlayerResponse> GetPlayerAsync(int id)
        {
            var player = await _context.Player.FindAsync(id);

            if (player == null)
            {
                return null;
            }

            var mapper = _mappingConfiguration.CreateMapper();
            return mapper.Map<PlayerResponse>(player);

        }


        public async Task<PlayerResponse> AddPlayer(PlayerRequest player)
        {
            // Important:
            // Validate Team, Add if not exists....
            await this.CheckTeamAsync(player.TeamName);

            var mapper = _mappingConfiguration.CreateMapper();

            var _player = mapper.Map<Player>(player);

            await _context.Player.AddAsync(_player);
            await _context.SaveChangesAsync();

            return mapper.Map<PlayerResponse>(_player);
        }

        public async Task<PlayerResponse> UpdatePlayer(PlayerRequest player, int id)
        {
            var existingPlayer = await _context.Player.FindAsync(id);
            if (existingPlayer == null)
            {
                return null;
            }

            // Important:
            // Validate Team, Add if not exists....
            await this.CheckTeamAsync(player.TeamName);

            var mapper = _mappingConfiguration.CreateMapper();

            mapper.Map(player, existingPlayer);
            _context.Player.Update(existingPlayer);
            await _context.SaveChangesAsync();

            return mapper.Map<PlayerResponse>(existingPlayer);
        }


        public async Task<PlayerResponse> RemovePlayer(int id)
        {
            var player = await _context.Player.FindAsync(id);
            if (player == null)
            {
                return null;
            }

            _context.Player.Remove(player);
            await _context.SaveChangesAsync();

            var mapper = _mappingConfiguration.CreateMapper();
            return mapper.Map<PlayerResponse>(player);

        }


        public async Task<PlayerResponse> FindAsync(int id)
        {

            var player = await _context.Player.FindAsync(id);

            var mapper = _mappingConfiguration.CreateMapper();
            return mapper.Map<PlayerResponse>(player);
        }


        public FXDataContext getContext()
        {
            return _context;
        }


        public async Task CheckTeamAsync(string name)
        {
            // Important:
            // Validate Team, Add if not exists....
            // TODO: Refactor and add in atomic transaction
            var team = await _context.Team.FindAsync(name);
            if (team == null)
            {
                await _context.Team.AddAsync(new Team { TeamName = name });
                // _context.SaveChangesAsync();
            }
        }

    }
}
