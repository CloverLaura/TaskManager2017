using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models;
using TaskManager.ViewModels;
using TaskManager2017.Models;

namespace TaskManager2017.Controllers
{
    public class TeamsController : Controller
    {
        private readonly TaskManager2017Context _context;

        public TeamsController(TaskManager2017Context context)
        {
            _context = context;
        }

        
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamID,Name,Description,CreatedBy")] Team team)
        {
            var team_ = _context.Team.FirstOrDefault(u => u.Name == team.Name);
            if (ModelState.IsValid & team_ == null)
            {
                string cookie = HttpContext.Request.Cookies["userCookie"];
                int userID = Convert.ToInt32(cookie);
                var user = _context.User.FirstOrDefault(u => u.UserID == userID);
                team.CreatedBy = user.Username;
                _context.Add(team);

               
                UserTeam userTeam = new UserTeam();
                userTeam.Team = team.Name;
                userTeam.User = user.Username;

                await _context.UserTeam.AddAsync(userTeam);
                _context.SaveChanges();
                return RedirectToAction("Home", "Users");
                
            }
            if(team_ != null)
            {
                ModelState.AddModelError("Name", "Your team name is already being used");
            }
            return View(team);
        }

        public IActionResult Details(string id)
        {
            var team = _context.Team.FirstOrDefault(u => u.Name == id);

            IQueryable<UserTeam> custQuery2 =
               from t in _context.UserTeam
               where t.Team == team.Name
               select t;
            List<UserTeam> usersInTeam = new List<UserTeam>();
            foreach (UserTeam teamU in custQuery2)
            {
                usersInTeam.Add(teamU);
            }
            ViewBag.UsersInTeam = usersInTeam;
            return View(team);
        }

        // GET: Teams/Edit/5
        /*public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Team.SingleOrDefaultAsync(m => m.TeamID == id);
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeamID,Name,Description,CreatedBy")] Team team)
        {
            if (id != team.TeamID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(team);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.TeamID))
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
            return View(team);
        }

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Team
                .SingleOrDefaultAsync(m => m.TeamID == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var team = await _context.Team.SingleOrDefaultAsync(m => m.TeamID == id);
            _context.Team.Remove(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(int id)
        {
            return _context.Team.Any(e => e.TeamID == id);
        }*/

        [HttpGet]
        public IActionResult JoinTeam()
        {
            
            List<Team> teams = _context.Team.ToList();
            List<UserTeam> userTeams = _context.UserTeam.ToList();
            string cookie = HttpContext.Request.Cookies["userCookie"];
            int userID = Convert.ToInt32(cookie);
            var user = _context.User.FirstOrDefault(u => u.UserID == userID);
            JoinTeamViewModel joinTeamViewModel = new JoinTeamViewModel();
            joinTeamViewModel.User = user;

            List<SelectListItem> dropTeams = new List<SelectListItem>();
            

            int value = 1;
            foreach (Team team in teams)
            {
                var userTeam = _context.UserTeam.FirstOrDefault(u => (u.User == user.Username) & (u.Team == team.Name));
                if (userTeam == null)
                {
                    dropTeams.Add(new SelectListItem { Text = team.Name, Value = team.Name });
                    value += 1;
                }

            }

            joinTeamViewModel.Teams = dropTeams;



            return View(joinTeamViewModel);
        }

        [HttpPost]
        public IActionResult JoinTeam(JoinTeamViewModel joinTeamViewModel, string Team)
        {
            string cookie = HttpContext.Request.Cookies["userCookie"];
            int userID = Convert.ToInt32(cookie);
            var user = _context.User.FirstOrDefault(u => u.UserID == userID);
            
            if (joinTeamViewModel.Team != null)
            {

                var team = _context.Team.FirstOrDefault(u => u.Name == Team);

                UserTeam userTeam = new UserTeam();
                userTeam.Team = team.Name;
                userTeam.User = user.Username;

                _context.UserTeam.AddAsync(userTeam);
                _context.SaveChanges();
                return RedirectToAction("Home", "Users");

            }
            ModelState.AddModelError("Team", "You must select a team");
            List<Team> teams = _context.Team.ToList();
            List<SelectListItem> dropTeams = new List<SelectListItem>();


            int value = 1;
            foreach (Team t in teams)
            {
                var userTeam = _context.UserTeam.FirstOrDefault(u => (u.User == user.Username) & (u.Team == t.Name));
                if (userTeam == null)
                {
                    dropTeams.Add(new SelectListItem { Text = t.Name, Value = t.Name });
                    value += 1;
                }

            }

            joinTeamViewModel.Teams = dropTeams;
            return View(joinTeamViewModel);
        }
    }
}
