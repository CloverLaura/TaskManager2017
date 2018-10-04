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
    public class TasksController : Controller
    {
        private readonly TaskManager2017dbContext _context;

        public TasksController(TaskManager2017dbContext context)
        {
            _context = context;
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskID,Name,Description,Project,Completed,IsTaken,TakenBy")] TaskManager.Models.Task task, string addNewTask, string finish)
        {
            if (ModelState.IsValid)
            {

                string cookie = HttpContext.Request.Cookies["projectCookie"];
                int projectID = Convert.ToInt32(cookie);
                var project = _context.Project.FirstOrDefault(u => u.ProjectID == projectID);
                if (ModelState.IsValid & string.IsNullOrEmpty(finish))
                {
                    task.Completed = false;
                    task.IsTaken = false;
                    task.TakenBy = "";
                    task.CreatedByInt = project.CreatedByInt;
                    task.Project = project.Name;
                    task.ProjectID = project.ProjectID;
                    project.TaskCount++;
                    _context.Add(task);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Create");


                }
                if(ModelState.IsValid & string.IsNullOrEmpty(addNewTask))
                {
                    task.Completed = false;
                    task.IsTaken = false;
                    task.TakenBy = "";
                    task.CreatedByInt = project.CreatedByInt;
                    task.Project = project.Name;
                    task.ProjectID = project.ProjectID;
                    project.TaskCount++;
                    _context.Add(task);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("ViewProjects", "Projects");
                }
                
            }
            return View(task);
        }

        public IActionResult FindTasks()
        {
            FindTasksViewModel findTasksViewModel = new FindTasksViewModel();
            List<Project> projects = _context.Project.ToList();
            List<TaskManager.Models.Task> tasks = _context.Task.ToList();
            List<Tuple<string,int>> tasksInProject = new List<Tuple<string, int>>();
            List<Tuple<string, string>> projectsCreatedBy = new List<Tuple<string, string>>();
            foreach(Project project in projects)
            {
                int numTasks = 0;
                foreach(TaskManager.Models.Task task in tasks)
                {
                    if(task.ProjectID == project.ProjectID)
                    {
                        numTasks++;
                    }
                }
                Tuple<string,int> projectTasks = Tuple.Create(project.Name, numTasks);
                tasksInProject.Add(projectTasks);
            }
        
            foreach (Project project in projects)
            {
                User user_ = _context.User.FirstOrDefault(u => u.UserID == project.CreatedByInt);
                Tuple<string, string> projectCreatedBy = Tuple.Create(project.Name, user_.Username);
                projectsCreatedBy.Add(projectCreatedBy);
            }
            findTasksViewModel.AllTasks = tasks;
            string cookie = HttpContext.Request.Cookies["userCookie"];
            int userID = Convert.ToInt32(cookie);
            var user = _context.User.FirstOrDefault(u => u.UserID == userID);

            findTasksViewModel.User = user;

            //IQueryable<UserTeam> custQuery =
                //from t in _context.UserTeam
                //where t.User == user.Username
                //select t;


            List<SelectListItem> dropTeams = new List<SelectListItem>();
            //dropTeams.Add(new SelectListItem { Text = "Please select Team", Value = "0" });

            //int value = 1;
            //foreach (UserTeam u in custQuery)
            //{
            //dropTeams.Add(new SelectListItem { Text = u.Team, Value = u.Team });
            //value += 1;
            //}

            //findTasksViewModel.Teams = dropTeams;
            findTasksViewModel.ProjectsCreatedBy = projectsCreatedBy;
            findTasksViewModel.TasksInProject = tasksInProject;
            return View(findTasksViewModel);
        }

        [HttpGet]
        public IActionResult FindTasks_ViewTasks(int id)
        {
            
            FindTasks_ViewTasksViewModel findTasks_ViewTasksViewModel = new FindTasks_ViewTasksViewModel();
            var project = _context.Project.FirstOrDefault(p => p.ProjectID == id);
            IQueryable<TaskManager.Models.Task> custQuery =
                from t in _context.Task
                where t.Project == project.Name
                select t;
            foreach (TaskManager.Models.Task task in custQuery)
            {
                findTasks_ViewTasksViewModel.Tasks.Add(task);
            }
            findTasks_ViewTasksViewModel.Project = project;
            return View(findTasks_ViewTasksViewModel);
        }

        [HttpPost]
        public IActionResult FindTasks_ViewTasks(FindTasks_ViewTasksViewModel findTasks_ViewTasksViewModel)
        {
            
            string cookie = HttpContext.Request.Cookies["userCookie"];
            int userID = Convert.ToInt32(cookie);
            var user = _context.User.FirstOrDefault(u => u.UserID == userID);
            var task = _context.Task.FirstOrDefault(t => t.TaskID == findTasks_ViewTasksViewModel.TaskID);
            task.IsTaken = true;
            task.TakenBy = user.Username;
            _context.SaveChangesAsync();
            return RedirectToAction("ViewTasks");
        }

        [HttpGet]
        public IActionResult ViewTasks()
        {
            
            string cookie = HttpContext.Request.Cookies["userCookie"];
            int userID = Convert.ToInt32(cookie);
            var user = _context.User.FirstOrDefault(u => u.UserID == userID);
            List<TaskManager.Models.Task> tasks = new List<TaskManager.Models.Task>();
            ViewTasksViewModel viewTasksViewModel = new ViewTasksViewModel();
            viewTasksViewModel.User = user;
            IQueryable<TaskManager.Models.Task> custQuery =
                from t in _context.Task
                where t.TakenBy == user.Username
                select t;
            foreach(var task in custQuery)
            {
                tasks.Add(task);
            }
            viewTasksViewModel.Tasks = tasks;

            return View(viewTasksViewModel);
        }

        [HttpPost]
        public IActionResult ViewTasks(ViewTasksViewModel viewTasksViewModel)
        {
            
            var task = _context.Task.FirstOrDefault(t => t.TaskID == viewTasksViewModel.TaskID);

            task.Completed = true;
            task.TakenBy = "";
            _context.SaveChangesAsync();
            return RedirectToAction("Home", "Users");
        }

        // GET: Tasks/Edit/5
        /*public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Task.SingleOrDefaultAsync(m => m.TaskID == id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskID,Name,Description,ProjectID,Completed,IsTaken,TakenBy")] TaskManager.Models.Task task)
        {
            if (id != task.TaskID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.TaskID))
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
            return View(task);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Task
                .SingleOrDefaultAsync(m => m.TaskID == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Task.SingleOrDefaultAsync(m => m.TaskID == id);
            _context.Task.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(int id)
        {
            return _context.Task.Any(e => e.TaskID == id);
        }*/
    }
}
