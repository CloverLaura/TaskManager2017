using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManager2017.Models;

namespace TaskManager2017.Controllers
{
    public class UserTeamsController : Controller
    {
        /*private readonly TaskManager2017Context _context;

        public UserTeamsController(TaskManager2017Context context)
        {
            _context = context;
        }


        // GET: UserTeams/Create
        public IActionResult Create(string teamName)
        {
            return View(); 
        }

        // POST: UserTeams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserTeamID,User,Team")] UserTeam userTeam)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userTeam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userTeam);
        }

        // GET: UserTeams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTeam = await _context.UserTeam.SingleOrDefaultAsync(m => m.UserTeamID == id);
            if (userTeam == null)
            {
                return NotFound();
            }
            return View(userTeam);
        }

        // POST: UserTeams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserTeamID,User,Team")] UserTeam userTeam)
        {
            if (id != userTeam.UserTeamID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userTeam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserTeamExists(userTeam.UserTeamID))
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
            return View(userTeam);
        }

        // GET: UserTeams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTeam = await _context.UserTeam
                .SingleOrDefaultAsync(m => m.UserTeamID == id);
            if (userTeam == null)
            {
                return NotFound();
            }

            return View(userTeam);
        }

        // POST: UserTeams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userTeam = await _context.UserTeam.SingleOrDefaultAsync(m => m.UserTeamID == id);
            _context.UserTeam.Remove(userTeam);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserTeamExists(int id)
        {
            return _context.UserTeam.Any(e => e.UserTeamID == id);
        }*/
    }
}
