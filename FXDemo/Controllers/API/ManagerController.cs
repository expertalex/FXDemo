using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FXDemo.Data;
using FXDemo.Models;
using AutoMapper;
using FXDemo.Models.Http;
using AutoMapper.QueryableExtensions;

namespace FXDemo.Controllers.API
{
    // TODO:
    // 1) Add IRefereeService (Repository Pattern)
    // 2) Add AutoMapper -> {Name}Request and {Name}Responce and remove manual id={id} maping

    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly FXDataContext _context;

        private readonly IConfigurationProvider _mappingConfiguration;

        public ManagerController(FXDataContext context, IConfigurationProvider mappingConfiguration)
        {
            _context = context;
            _mappingConfiguration = mappingConfiguration;
        }

        // GET: api/Manager
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ManagerResponse>>> GetManager()
        {

            var query = _context.Manager.ProjectTo<ManagerResponse>(_mappingConfiguration);
            var responce = await query.ToListAsync();

            return responce;
        }

        // GET: api/Manager/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ManagerResponse>> GetManager(int id)
        {
            // Important in order to alow -+ ids being passed to controler
            var manager = await _context.Manager.FindAsync(Math.Abs(id));

            if (manager == null)
            {
                return NotFound();
            }

            var mapper = _mappingConfiguration.CreateMapper();
            var responce = mapper.Map<ManagerResponse>(manager);
            return responce;
        }

        // PUT: api/Manager/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutManager(int id, ManagerRequest manager)
        {

            // manager = ManagerRequest(manager, id);

            if (manager == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var existingManager = await _context.Manager.FindAsync(Math.Abs(id));
            if (existingManager == null)
            {
                return NotFound();
            }

            var mapper = _mappingConfiguration.CreateMapper();

            try
            {
                // Important:
                // Validate Team, Add if not exists....
                await this.CheckTeamAsync(manager.TeamName);
                
                mapper.Map(manager, existingManager);
                _context.Manager.Update(existingManager);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManagerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
            return NoContent();
        }

        // POST: api/Manager
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ManagerResponse>> PostManager(ManagerRequest manager)
        {

            // manager = ManagerRequest(manager);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var mapper = _mappingConfiguration.CreateMapper();
            var createdManager = mapper.Map<Manager>(manager);

            // Important:
            // Validate Team, Add if not exists....
            await this.CheckTeamAsync(manager.TeamName);

            _context.Manager.Add(createdManager);
            await _context.SaveChangesAsync();


            var managerRsponce = mapper.Map<ManagerResponse>(createdManager);
            // managerRsponce.Id = -managerRsponce.Id;

            return CreatedAtAction("GetManager", new { id = managerRsponce.Id }, managerRsponce);
        }

        // DELETE: api/Manager/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ManagerResponse>> DeleteManager(int id)
        {
            var manager = await _context.Manager.FindAsync(Math.Abs(id));
            if (manager == null)
            {
                return NotFound();
            }

            _context.Manager.Remove(manager);
            await _context.SaveChangesAsync();

            var mapper = _mappingConfiguration.CreateMapper();

            return mapper.Map<ManagerResponse>(manager);
        }


        public async Task CheckTeamAsync(string name)
        {
            // Important:
            // Validate Team, Add if not exists....
            // TODO: Refactor
            var team = await _context.Team.FindAsync(name);
            if (team == null)
            {
                _context.Team.Add(new Team { TeamName = name });
                // _context.SaveChangesAsync();
            }
        }


        private bool ManagerExists(int id)
        {
            return _context.Manager.Any(e => e.Id == Math.Abs(id));
        }
    }
}
