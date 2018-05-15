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

        // GET: Teams

        /*public async Task<IActionResult> Index()
        {
            return View(await _context.Team.ToListAsync());
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int? id)
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
        }*/

        // GET: Teams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

                //string cookie = HttpContext.Request.Cookies["userCookie"];
                //int userID = Convert.ToInt32(cookie);
                //var user = _context.User.FirstOrDefault(u => u.UserID == userID);
                UserTeam userTeam = new UserTeam();
                userTeam.Team = team.Name;
                userTeam.User = user.Username;

                await _context.UserTeam.AddAsync(userTeam);
                _context.SaveChanges();
                return RedirectToAction("Home", "Users");
                //await _context.SaveChangesAsync();
                //return RedirectToAction("Create", "UserTeams", new { teamName = team.Name });
            }
            if(team_ != null)
            {
                ModelState.AddModelError("Name", "Your team name is already being used");
            }
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
            //UserData userData = new UserData();
            //TeamData teamData = new TeamData();
            //List<Team> teams = teamData.TeamsToList();
            List<Team> teams = _context.Team.ToList();
            List<UserTeam> userTeams = _context.UserTeam.ToList();
            string cookie = HttpContext.Request.Cookies["userCookie"];
            int userID = Convert.ToInt32(cookie);
            var user = _context.User.FirstOrDefault(u => u.UserID == userID);
            //User user = userData.GetById(userID);
            JoinTeamViewModel joinTeamViewModel = new JoinTeamViewModel();
            joinTeamViewModel.User = user;

            List<SelectListItem> dropTeams = new List<SelectListItem>();
            dropTeams.Add(new SelectListItem { Text = "Please select Team", Value = "0" });

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
        public IActionResult JoinTeam(JoinTeamViewModel joinTeamViewModel)
        {
            //TeamData teamData = new TeamData();
            //UserData userData = new UserData();
            //Team selectedTeam = joinTeamViewModel.Team;
            //User user = joinTeamViewModel.User;
            string cookie = HttpContext.Request.Cookies["userCookie"];
            int userID = Convert.ToInt32(cookie);
            var user = _context.User.FirstOrDefault(u => u.UserID == userID);
            var team = _context.Team.FirstOrDefault(u => u.Name == joinTeamViewModel.Team);
            //Team team = teamData.FindByName(joinTeamViewModel.Team);
            //User user = userData.GetById(userID);
            //team.UsersInTeam.Add(user);
            //userData.AddTeam(user, joinTeamViewModel.Team);
            UserTeam userTeam = new UserTeam();
            userTeam.Team = team.Name;
            userTeam.User = user.Username;

            _context.UserTeam.AddAsync(userTeam);
            _context.SaveChanges();
            return RedirectToAction("Home", "Users");

            //return RedirectToAction("Home", "Login", new { email = user.Email });
        }
    }
}
