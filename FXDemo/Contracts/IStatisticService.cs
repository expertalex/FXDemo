using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FXDemo.Data;
using FXDemo.Models;
using FXDemo.Models.Http;

namespace FXDemo.Contracts
{
    public interface IStatisticService
    {
        // TODO: Implement


        Task<IEnumerable<CardResponse>> GetRedCardsAsync();
        Task<IEnumerable<CardResponse>> GetYellowCardsAsync();
        Task<IEnumerable<MinuteResponse>> GetMinutesPlayAsync();

    }
}
