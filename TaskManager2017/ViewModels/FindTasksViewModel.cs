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

        public FindTasksViewModel()
        {
            AllProjects = new List<Project>();
        }
    }

    
}
