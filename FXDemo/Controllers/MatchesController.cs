using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FXDemo.Data;
using FXDemo.Models;

namespace FXDemo.Controllers
{
    public class MatchesController : Controller
    {
        private readonly FXDataContext _context;

        public MatchesController(FXDataContext context)
        {
            _context = context;
        }

        // GET: Matches
        public async Task<IActionResult> Index()
        {
            var fXDataContext = _context.Match.Include(m => m.AwayTeamManager).Include(m => m.HouseTeamManager).Include(m => m.Referee);
            return View(await fXDataContext.ToListAsync());
        }

        // GET: Matches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Match
                .Include(m => m.AwayTeamManager)
                .Include(m => m.HouseTeamManager)
                .Include(m => m.Referee)
                .Include(m => m.HouseTeamPlayers).ThenInclude(m => m.Player)
                .Include(m => m.AwayTeamPlayers).ThenInclude(m => m.Player)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // GET: Matches/Create
        public IActionResult Create()
        {
            ViewData["AwayTeamManagerId"] = new SelectList(_context.Manager, "Id", "Name");
            ViewData["HouseTeamManagerId"] = new SelectList(_context.Manager, "Id", "Name");
            ViewData["RefereeId"] = new SelectList(_context.Referee, "Id", "Name");
            return View();
        }

        // POST: Matches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,HouseTeamManagerId,AwayTeamManagerId,RefereeId,Date")] Match match)
        {
            if (ModelState.IsValid)
            {
                _context.Add(match);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AwayTeamManagerId"] = new SelectList(_context.Manager, "Id", "Name", match.AwayTeamManagerId);
            ViewData["HouseTeamManagerId"] = new SelectList(_context.Manager, "Id", "Name", match.HouseTeamManagerId);
            ViewData["RefereeId"] = new SelectList(_context.Referee, "Id", "Name", match.RefereeId);
            return View(match);
        }

        // GET: Matches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Match.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }
            ViewData["AwayTeamManagerId"] = new SelectList(_context.Manager, "Id", "Name", match.AwayTeamManagerId);
            ViewData["HouseTeamManagerId"] = new SelectList(_context.Manager, "Id", "Name", match.HouseTeamManagerId);
            ViewData["RefereeId"] = new SelectList(_context.Referee, "Id", "Name", match.RefereeId);
            return View(match);
        }

        // POST: Matches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,HouseTeamManagerId,AwayTeamManagerId,RefereeId,Date")] Match match)
        {
            if (id != match.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(match);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchExists(match.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AwayTeamManagerId"] = new SelectList(_context.Manager, "Id", "Name", match.AwayTeamManagerId);
            ViewData["HouseTeamManagerId"] = new SelectList(_context.Manager, "Id", "Name", match.HouseTeamManagerId);
            ViewData["RefereeId"] = new SelectList(_context.Referee, "Id", "Name", match.RefereeId);
            return View(match);
        }

        // GET: Matches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Match
                .Include(m => m.AwayTeamManager)
                .Include(m => m.HouseTeamManager)
                .Include(m => m.Referee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var match = await _context.Match.FindAsync(id);
            _context.Match.Remove(match);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchExists(int id)
        {
            return _context.Match.Any(e => e.Id == id);
        }
    }
}
