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
using TaskManager2017.ViewModels;

namespace TaskManager2017.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly TaskManager2017Context _context;

        public ProjectsController(TaskManager2017Context context)
        {
            _context = context;
        }

        
        public IActionResult Create()
        {
            string cookie = HttpContext.Request.Cookies["userCookie"];
            int userID = Convert.ToInt32(cookie);
            var user = _context.User.FirstOrDefault(u => u.UserID == userID);
            CreateProjectViewModel createProjectViewModel = new CreateProjectViewModel();
            IQueryable<Team> custQuery =
                from t in _context.Team
                where t.CreatedBy == user.Username
                select t;
            List<SelectListItem> dropTeams = new List<SelectListItem>();
            //dropTeams.Add(new SelectListItem { Text = "Please select Team", Value = "" });
            
            foreach (Team team in custQuery)
            {
                dropTeams.Add(new SelectListItem { Text = team.Name, Value = team.Name });
                
            }
            
            ViewBag.TeamP = dropTeams;

            
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectID,Name,Description,CreatedBy,Team")] Project project, string TeamP)
        {
            var project_ = _context.Project.FirstOrDefault(u => u.Name == project.Name);
            if (ModelState.IsValid & project_ == null)
            {
                string cookie = HttpContext.Request.Cookies["userCookie"];
                int userID = Convert.ToInt32(cookie);
                var user = _context.User.FirstOrDefault(u => u.UserID == userID);
                project.CreatedBy = user.Username;
                project.TeamP = TeamP;
                _context.Add(project);
                await _context.SaveChangesAsync();
                Response.Cookies.Append("projectCookie", project.ProjectID.ToString());
                return RedirectToAction("Create","Tasks");
            }
            if(project_ != null)
            {
                ModelState.AddModelError("Name", "Your project name has already been taken");
            }
            return View(project);
        }

        public IActionResult ViewProjects()
        {
            ViewProjectsViewModel viewProjectsViewModel = new ViewProjectsViewModel();
            string cookie = HttpContext.Request.Cookies["userCookie"];
            int userID = Convert.ToInt32(cookie);
            var user = _context.User.FirstOrDefault(u => u.UserID == userID);

            string cookieP = HttpContext.Request.Cookies["projectCookie"];
            int projectID = Convert.ToInt32(cookie);
            var project = _context.Project.FirstOrDefault(u => u.ProjectID == projectID);

            List<Project> projects = _context.Project.ToList();
            List<TaskManager.Models.Task> tasks = _context.Task.ToList();


            List<Project> userProjects = new List<Project>();
            foreach (Project p in projects)
            {
                if (p.CreatedBy == user.Username)
                {
                    userProjects.Add(p);
                }
            }

            viewProjectsViewModel.userProjects = userProjects;
            viewProjectsViewModel.allTasks = tasks;
            return View(viewProjectsViewModel);
        }

        public IActionResult TeamProjects(string Team)
        {
            TeamProjectsViewModel teamProjectsViewModel = new TeamProjectsViewModel();
            var selectedTeam = _context.Team.FirstOrDefault(u => u.Name == Team);
            teamProjectsViewModel.Team = selectedTeam;
            IQueryable<Project> custQuery =
                from p in _context.Project
                where p.TeamP == Team
                select p;
            foreach (Project project in custQuery)
            {
                teamProjectsViewModel.AllProjects.Add(project);
            }
            
            return View(teamProjectsViewModel);
        }

        /*// GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project.SingleOrDefaultAsync(m => m.ProjectID == id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectID,Name,Description,CreatedBy")] Project project)
        {
            if (id != project.ProjectID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ProjectID))
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
            return View(project);
        }*/

        // GET: Projects/Delete/5
        /*public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .SingleOrDefaultAsync(m => m.ProjectID == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Project.SingleOrDefaultAsync(m => m.ProjectID == id);
            _context.Project.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.ProjectID == id);
        }*/
    }
}
