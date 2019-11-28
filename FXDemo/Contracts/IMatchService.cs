using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FXDemo.Data;
using FXDemo.Models;
using FXDemo.Models.Http;

namespace FXDemo.Contracts
{
    public interface IMatchService
    {
        // TODO: Implement

        // TODO: Remove
        FXDataContext getContext();

        Task<MatchResponse> GetMatcheAsync(int id);

        Task<IEnumerable<MatchResponse>> GetMatcheAsync();

        Task<MatchResponse> AddMatchAsync(MatchRequest match);

        Task<MatchResponse> UpdateMatchAsync(MatchRequest match, int id);

        Task<MatchResponse> RemoveMatchAsync(int id);

        Task<MatchResponse> FindMatchAsync(int id);

    }
}
