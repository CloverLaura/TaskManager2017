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

        /*public IActionResult AddTasks(int projectID)
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
        }*/

        

        

        

    }
}
