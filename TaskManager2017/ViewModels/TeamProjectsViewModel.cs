using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.ViewModels
{
    public class TeamProjectsViewModel
    {
        public List<Project> AllProjects { get; set; }

        public Team Team { get; set; }

        public TeamProjectsViewModel()
        {
            AllProjects = new List<Project>();
        }
    }
}
