using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FXDemo.Data;
using FXDemo.Models;
using FXDemo.Models.Http;
using Microsoft.AspNetCore.Mvc;

namespace FXDemo.Contracts
{
    public interface IPlayerService
    {

        // TODO: Remove
        FXDataContext getContext();

        Task<IEnumerable<PlayerResponse>> GetPlayersAsync();

        Task<PlayerResponse> GetPlayerAsync(int id);

        Task<PlayerResponse> AddPlayer(PlayerRequest player);

        Task<PlayerResponse> UpdatePlayer(PlayerRequest player);

        Task<PlayerResponse> RemovePlayer(int id);

        Task<PlayerResponse> FindAsync(int id);
    }
}
