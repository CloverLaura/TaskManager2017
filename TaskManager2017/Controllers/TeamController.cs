using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManager.ViewModels;
using TaskManager.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManager2017.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManager.Controllers
{
    public class TeamController : Controller
    {
        private readonly TaskManager2017Context _context;

        public TeamController(TaskManager2017Context context)
        {
            _context = context;
        }

        /*public IActionResult AddTeam()
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
                    CreatedBy = user.Username

                };
                teamData.Add(newTeam);
                userData.AddTeam(user, newTeam.Name);
                return RedirectToAction("Home", "Login", new { email = user.Email });
            }
            else
            {
                return View(addTeamViewModel);
            }
        }*/

        
    }
}
