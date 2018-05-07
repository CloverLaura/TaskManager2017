using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;
using TaskManager.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManager2017.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManager.Controllers
{
    public class TaskController : Controller
    {

        private readonly TaskManager2017Context _context;

        public TaskController(TaskManager2017Context context)
        {
            _context = context;
        }

        public IActionResult AddTasks(int projectID)
        {
            ProjectData projectData = new ProjectData();
            Project project = projectData.GetByID(projectID);
            AddTasksViewModel addTasksViewModel = new AddTasksViewModel();
            return View(addTasksViewModel);
        }

        [HttpPost]
        public ActionResult AddTasks(AddTasksViewModel obj, string addNewTask, string finish)
        {
            ProjectData projectData = new ProjectData();
            TaskData taskData = new TaskData();
            UserData userData = new UserData();
            int projectID = Convert.ToInt32(HttpContext.Request.Cookies["projectCookie"]);
            Project project = projectData.GetByID(projectID);
            if (project.Tasks.Count() >= 1 & !string.IsNullOrEmpty(finish))
            {
                if (ModelState.IsValid)
                {
                    Models.Task task = new Models.Task
                    {
                        Name = obj.Name,
                        Description = obj.Description,
                        //Project = project
                    };

                    taskData.AddTask(project, task);
                    taskData.Add(task);
                }
                return RedirectToAction("Home", "Login");
            }
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(addNewTask))
                {


                    Models.Task task = new Models.Task();
                    task.Name = obj.Name;
                    task.Description = obj.Description;
                    //task.Project = project;
                    taskData.AddTask(project, task);
                    taskData.Add(task);
                    return RedirectToAction("AddTasks", "Task", new { projectID = projectID });


                }
                if (!string.IsNullOrEmpty(finish))
                {
                    Models.Task task = new Models.Task
                    {
                        Name = obj.Name,
                        Description = obj.Description,
                        //Project = project
                    };

                    taskData.AddTask(project, task);
                    taskData.Add(task);

                }
            }

            else
            {
                return View(obj);
            }

            string cookie = HttpContext.Request.Cookies["userCookie"];
            int userID = Convert.ToInt32(cookie);
            User user = userData.GetById(userID);
            return RedirectToAction("Home", "Login", new { email = user.Email });
        }

        public IActionResult FindTasks()
        {
            FindTasksViewModel findTasksViewModel = new FindTasksViewModel();
            //ProjectData projectData = new ProjectData();
            List<Project> projects = _context.Project.ToList();
            List<Models.Task> tasks = _context.Task.ToList();
            //List<Project> allProjects = projectData.ViewAllProjects();
            foreach (Project project in projects)
            {
                findTasksViewModel.AllProjects.Add(project);
            }
            findTasksViewModel.AllTasks = tasks;
            //UserData userData = new UserData();
            string cookie = HttpContext.Request.Cookies["userCookie"];
            int userID = Convert.ToInt32(cookie);
            //User user = userData.GetById(userID);
            var user = _context.User.FirstOrDefault(u => u.UserID == userID);

            findTasksViewModel.User = user;


            List<SelectListItem> dropTeams = new List<SelectListItem>();
            dropTeams.Add(new SelectListItem { Text = "Please select Team", Value = "0" });

            int value = 1;
            foreach (Team team in user.UserTeams)
            {
                dropTeams.Add(new SelectListItem { Text = team.Name, Value = team.Name });
                value += 1;
            }

            findTasksViewModel.Teams = dropTeams;

            return View(findTasksViewModel);
        }

        [HttpGet]
        public IActionResult FindTasks_ViewTasks(int id)
        {
            //ProjectData projectData = new ProjectData();
            FindTasks_ViewTasksViewModel findTasks_ViewTasksViewModel = new FindTasks_ViewTasksViewModel();
            //Project project = projectData.GetByID(id);
            var project = _context.Project.FirstOrDefault(p => p.ProjectID == id);
            IQueryable<Models.Task> custQuery =
                from t in _context.Task
                where t.ProjectID == project.ProjectID
                select t;
            foreach(Models.Task task in custQuery)
            {
                findTasks_ViewTasksViewModel.Tasks.Add(task);
            }
            findTasks_ViewTasksViewModel.Project = project;
            return View(findTasks_ViewTasksViewModel);
        }

        [HttpPost]
        public IActionResult FindTasks_ViewTasks(FindTasks_ViewTasksViewModel findTasks_ViewTasksViewModel)
        {
            //TaskData taskData = new TaskData();
            //UserData userData = new UserData();
            string cookie = HttpContext.Request.Cookies["userCookie"];
            int userID = Convert.ToInt32(cookie);
            var user = _context.User.FirstOrDefault(u => u.UserID == userID);
            var task = _context.Task.FirstOrDefault(t => t.TaskID == findTasks_ViewTasksViewModel.TaskID);
            //Models.Task task = taskData.GetByID(findTasks_ViewTasksViewModel.TaskID);
            task.IsTaken = true;
            task.TakenBy = user.Username;
            //userData.AddTask(user, task);
            _context.SaveChangesAsync();
            return RedirectToAction("ViewTasks");
        }

        [HttpGet]
        public IActionResult ViewTasks()
        {
            /*UserData userData = new UserData();
            ProjectData projectData = new ProjectData();*/
            string cookie = HttpContext.Request.Cookies["userCookie"];
            int userID = Convert.ToInt32(cookie);
            //User user = userData.GetById(userID);
            var user = _context.User.FirstOrDefault(u => u.UserID == userID);
            List<Models.Task> tasks = _context.Task.ToList();
            ViewTasksViewModel viewTasksViewModel = new ViewTasksViewModel();
            viewTasksViewModel.User = user;
            viewTasksViewModel.Tasks = tasks;

            return View(viewTasksViewModel);
        }

        [HttpPost]
        public IActionResult ViewTasks(ViewTasksViewModel viewTasksViewModel)
        {
            //TaskData taskData = new TaskData();
            //Models.Task task = taskData.GetByID(viewTasksViewModel.TaskID);
            var task = _context.Task.FirstOrDefault(t => t.TaskID == viewTasksViewModel.TaskID);

            task.Completed = true;
            task.TakenBy = "";
            _context.SaveChangesAsync();
            //UserData userData = new UserData();
            //string cookie = HttpContext.Request.Cookies["userCookie"];
            //int userID = Convert.ToInt32(cookie);
            //User user = userData.GetById(userID);

            //user.UserTasks.Remove(task);
            


            return RedirectToAction("Home", "Login");
        }

    }
}
