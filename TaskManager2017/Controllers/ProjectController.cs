using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManager.ViewModels;
using TaskManager.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManager2017.Models;
using TaskManager2017.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManager.Controllers
{
    public class ProjectController : Controller
    {
        public IEnumerable<SelectListItem> GetSelectListItems { get; set; }

        private readonly TaskManager2017Context _context;

        public ProjectController(TaskManager2017Context context)
        {
            _context = context;
        }

        /*// GET: /<controller>/
        public IActionResult AddProject()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProject(AddViewModel addViewModel)
        {
            if (ModelState.IsValid)
            {
                ProjectData projectData = new ProjectData();
                UserData userData = new UserData();
                TeamData teamData = new TeamData();

                string cookie = HttpContext.Request.Cookies["userCookie"];
                int userID = Convert.ToInt32(cookie);
                User user = userData.GetById(userID);

                Project project = new Project
                {
                    Name = addViewModel.Name,
                    Description = addViewModel.Description,
                    CreatedBy = user.Username

                };
                projectData.Add(project);
                Response.Cookies.Append("projectCookie", project.ProjectID.ToString());
                userData.AddProject(user, project);

                if (user.UserTeams.Count() != 0)
                {
                    foreach (Team team in user.UserTeams)
                    {
                        if (user.Username == team.CreatedBy)
                        {
                            teamData.AddProjectToTeam(team, project);
                        }
                    }
                }
                return RedirectToAction("AddTasks", "Task", new { projectID = project.ProjectID });
            }
            return View(addViewModel);


        }*/

        

        


    }
}
