using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManager.ViewModels;
using TaskManager.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManager.Controllers
{
    public class TeamController : Controller
    {
        public IActionResult AddTeam()
        {
            return View(new AddTeamViewModel());
        }

        [HttpPost]
        public IActionResult AddTeam(AddTeamViewModel addTeamViewModel, string submit)
        {
            UserData userData = new UserData();
            TeamData teamData = new TeamData();
            if(teamData.FindByName(addTeamViewModel.Name) != null & addTeamViewModel.Name != null)
            {
                ModelState.AddModelError("Name", "Team name already being used");
                return View(addTeamViewModel);
            }
            if (ModelState.IsValid)
            {
                User user = userData.GetById(Convert.ToInt32(HttpContext.Request.Cookies["userCookie"]));
                Team newTeam = new Team
                {
                    Name = addTeamViewModel.Name,
                    Description = addTeamViewModel.Description,
                    CreatedBy = user.UserID

                };
                teamData.Add(newTeam);
                userData.AddTeam(user, newTeam.Name);
                return RedirectToAction("Home", "Login", new { email = user.Email });
            }
            else
            {
                return View(addTeamViewModel);
            }
        }

        [HttpGet]
        public IActionResult JoinTeam()
        {
            UserData userData = new UserData();
            TeamData teamData = new TeamData();
            List<Team> teams = teamData.TeamsToList();
            string cookie = HttpContext.Request.Cookies["userCookie"];
            int userID = Convert.ToInt32(cookie);
            User user = userData.GetById(userID);
            JoinTeamViewModel joinTeamViewModel = new JoinTeamViewModel();
            joinTeamViewModel.User = user;

            List<SelectListItem> dropTeams = new List<SelectListItem>();
            dropTeams.Add(new SelectListItem { Text = "Please select Team", Value = "0" });

            int value = 1;
            foreach (Team team in teams)
            {
                if (!user.UserTeams.Contains(team))
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
            TeamData teamData = new TeamData();
            UserData userData = new UserData();
            //Team selectedTeam = joinTeamViewModel.Team;
            //User user = joinTeamViewModel.User;
            string cookie = HttpContext.Request.Cookies["userCookie"];
            int userID = Convert.ToInt32(cookie);
            Team team = teamData.FindByName(joinTeamViewModel.Team);
            User user = userData.GetById(userID);
            team.UsersInTeam.Add(user);
            userData.AddTeam(user, joinTeamViewModel.Team);

            return RedirectToAction("Home", "Login", new { email = user.Email });
        }
    }
}
