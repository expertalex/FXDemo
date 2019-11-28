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
using AutoMapper;

namespace FXDemo.Controllers.API
{

    // TODO:
    // 1) Add IRefereeService (Repository Pattern)
    // 2) Add AutoMapper -> {Name}Request and {Name}Responce and remove manual id={id} maping
    [Route("api/[controller]")]
    [ApiController]
    public class RefereeController : ControllerBase
    {
        private readonly FXDataContext _context;
        private readonly IConfigurationProvider _mappingConfiguration;

        public RefereeController(FXDataContext context, IConfigurationProvider mappingConfiguration)
        {
            _context = context;
            _mappingConfiguration = mappingConfiguration;
        }

        // GET: api/Referee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Referee>>> GetReferee()
        {
            return await _context.Referee.ToListAsync();
        }

        // GET: api/Referee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Referee>> GetReferee(int id)
        {
            var referee = await _context.Referee.FindAsync(id);

            if (referee == null)
            {
                return NotFound();
            }

            return referee;
        }

        // PUT: api/Referee/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReferee(int id, RefereeRequest referee)
        {

            // TODO: Refactor in Request and Reponce (Quico fix)
            // referee.Id = id;

            if (referee == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var existingReferee = await _context.Referee.FindAsync(id);
            if (existingReferee == null)
            {
                return NotFound();
            }

            var mapper = _mappingConfiguration.CreateMapper();
            // _context.Entry(referee).State = EntityState.Modified;

            try
            {
                mapper.Map(referee, existingReferee); 
                _context.Referee.Update(existingReferee);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RefereeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Referee
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Referee>> PostReferee(RefereeRequest referee)
        {
            // TODO: Refactor in Request and Reponce (Quico fix)
            // referee.Id = 0;

            var mapper = _mappingConfiguration.CreateMapper();


            var createdRefere = mapper.Map<Referee>(referee);

            _context.Referee.Add(createdRefere);
            await _context.SaveChangesAsync();

            // TODO: Return 200???? 
            return CreatedAtAction("GetReferee", new { id = createdRefere.Id }, createdRefere);
        }

        // DELETE: api/Referee/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Referee>> DeleteReferee(int id)
        {
            var referee = await _context.Referee.FindAsync(id);
            if (referee == null)
            {
                return NotFound();
            }

            _context.Referee.Remove(referee);
            await _context.SaveChangesAsync();

            return referee;
        }

        private bool RefereeExists(int id)
        {
            return _context.Referee.Any(e => e.Id == id);
        }
    }
}
