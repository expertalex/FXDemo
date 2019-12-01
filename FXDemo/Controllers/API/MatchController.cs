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
using FXDemo.Contracts;

namespace FXDemo.Controllers.API
{

    // TODO:
    // 1) Add IRefereeService (Repository Pattern)
    // 2) Add AutoMapper -> {Name}Request and {Name}Responce and remove manual id={id} maping
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
   
        private readonly IMatchService _service;

        public MatchController(IMatchService service)
        {
            _service = service;
        }

        // GET: api/Match
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MatchResponse>>> GetMatch()
        {
            var response = await _service.GetMatcheAsync();
            return Ok(response);
        }

        // GET: api/Match/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MatchResponse>> GetMatch(int id)
        {
            var match = await _service.GetMatcheAsync(id);

            if (match == null)
            {
                return NotFound();
            }

            return Ok(match);
        }

        // PUT: api/Match/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatch(int id, MatchRequest match)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // _context.Entry(match).State = EntityState.Modified;

            try
            {
                var responce = await _service.UpdateMatchAsync(match, id);
                if (responce == null)
                {
                    return BadRequest(ModelState);
                }
                return Ok(responce);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // return Ok();
            // return NoContent();
        }

        // POST: api/Match
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<MatchResponse>> PostMatch(MatchRequest match)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var matchResponce = await _service.AddMatchAsync(match);
            if (matchResponce == null)
            {
                return BadRequest(ModelState);
            }

            return Ok(matchResponce);

            // return CreatedAtAction("GetMatch", new { id = matchResponce.Id }, matchResponce);
        }

        // DELETE: api/Match/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MatchResponse>> DeleteMatch(int id)
        {
            var match = await _service.RemoveMatchAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            return Ok();
        }

        private bool MatchExists(int id)
        {
            // TODO: Refactor Remove
            return _service.getContext().Match.Any(e => e.Id == id);
        }
    }
}
