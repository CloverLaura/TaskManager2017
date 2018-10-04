using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.ViewModels
{
    public class FindTasksViewModel
    {
        public User User { get; set; }

        public string TeamName { get; set; }

        public IEnumerable<SelectListItem> Teams { get; set; }

        public List<Project> AllProjects { get; set; }

        public List<Models.Task> AllTasks { get; set; }

        public int Counter { get; set; }

        public List<Tuple<string, int>> TasksInProject { get; set; }

        public List<Tuple<string, string>> ProjectsCreatedBy { get; set; }

        public FindTasksViewModel()
        {
            AllProjects = new List<Project>();
            AllTasks = new List<Models.Task>();
            Counter = 0;

            //Dictionary<Project, int> TasksInProject = new Dictionary<Project, int>();
            //Dictionary<Project, string> ProjectsCreatedBy = new Dictionary<Project, string>();
        }
    }

    
}
