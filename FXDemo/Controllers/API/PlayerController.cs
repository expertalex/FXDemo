using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FXDemo.Data;
using FXDemo.Models;
using FXDemo.Models.Http;
using System.Net;
using FXDemo.Contracts;

namespace FXDemo.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {

        private readonly IPlayerService _service;

        public PlayerController(IPlayerService service)
        {
            _service = service;
        }

        // GET: api/Player
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerResponse>>> GetPlayer()
        {
            // return await _service.GetPlayersAsync();
            var players = await _service.GetPlayersAsync();

            return Ok(players);

        }

        // GET: api/Player/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerResponse>> GetPlayer([FromRoute] int id)
        {
            var player = await _service.GetPlayerAsync(id);

            if (player == null)
            {
                return NotFound();
            }

            return Ok(player);
        }

        // PUT: api/Player/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<ActionResult<PlayerResponse>> PutPlayer([FromRoute] int id, [FromBody] PlayerRequest player)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // _service.getContext().Entry(player).State = EntityState.Modified;

            try
            {
                var playerResponce = await _service.UpdatePlayer(player, id);
                if(playerResponce == null)
                {
                    return NotFound();
                }
                return Ok(playerResponce);
            }
            catch (DbUpdateConcurrencyException)
            {
                var playerFound = await _service.FindAsync(id);
                if (playerFound == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

           
        }

        // POST: api/Player
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PlayerResponse>> PostPlayer([FromBody] PlayerRequest player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var playerResponce = await _service.AddPlayer(player);

            return CreatedAtAction("GetPlayer", new { id = playerResponce.Id }, playerResponce);
        }

        // DELETE: api/Player/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PlayerResponse>> DeletePlayer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var player = await _service.RemovePlayer(id);
            if (player== null)
            {
                return NotFound();
            }

            return Ok();
            // return player;
        }

    }
}
