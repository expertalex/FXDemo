using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FXDemo.Data;
using FXDemo.Models;
using FXDemo.Models.Http;

namespace FXDemo.Contracts
{
    public interface IPlayerService
    {

        Task<IEnumerable<PlayerResponse>> GetPlayersAsync();

        Task<PlayerResponse> GetPlayerAsync(int id);

        Task<PlayerResponse> AddPlayer(PlayerRequest player);

        Task<PlayerResponse> UpdatePlayer(PlayerRequest player, int id);

        Task<PlayerResponse> RemovePlayer(int id);

        Task<PlayerResponse> FindAsync(int id);

    }
}
