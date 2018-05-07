using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager2017.ViewModels
{
    public class ViewProjectsViewModel
    {
        public List<Project> userProjects { get; set; }
        public List<TaskManager.Models.Task> allTasks { get; set; }
    }
}
