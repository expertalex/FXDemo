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
    public class StatisticService : IStatisticService
    {
        private readonly FXDataContext _context;
        private readonly AutoMapper.IConfigurationProvider _mappingConfiguration;

        public StatisticService(
            FXDataContext context, AutoMapper.IConfigurationProvider mappingConfiguration)
        {
            _context = context;
            _mappingConfiguration = mappingConfiguration;
        }

        public async Task<IEnumerable<MinuteResponse>> GetMinutesPlayAsync()
        {
            var responce = await _context.Player.ProjectTo<MinuteResponse>(_mappingConfiguration).ToListAsync();
            var referees = await _context.Referee.ProjectTo<MinuteResponse>(_mappingConfiguration).ToListAsync();

            responce.AddRange(referees);

            return responce;
        }

        public async Task<IEnumerable<CardResponse>> GetRedCardsAsync()
        {
            var responce = await _context.Player.ProjectTo<YelowCardResponse>(_mappingConfiguration).ToListAsync();
            var managers = await _context.Manager.ProjectTo<YelowCardResponse>(_mappingConfiguration).ToListAsync();

            responce.AddRange(managers);

            return responce;
        }

        public async Task<IEnumerable<CardResponse>> GetYellowCardsAsync()
        {
            var responce = await _context.Player.ProjectTo<RedCardResponse>(_mappingConfiguration).ToListAsync();
            var managers = await _context.Manager.ProjectTo<RedCardResponse>(_mappingConfiguration).ToListAsync();

            responce.AddRange(managers);

            return responce;

        }
    }
}
