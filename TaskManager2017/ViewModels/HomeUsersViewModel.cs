using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager2017.ViewModels
{
    public class HomeUsersViewModel
    {
        public IEnumerable<string> UserCreatedTeams { get; set; }
        public List<Team> TeamsUserIn { get; set; }
        public List<Project> UserProjects { get; set; }
        public List<TaskManager.Models.Task> UserTasks { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CompletedTasks { get; set; }
        public int UncompletedTasks { get; set; }
        public string UserName { get; set; }

        public HomeUsersViewModel()
        {
            //IEnumerable<string> UserCreatedTeams;
            List<Team> TeamsUserIn = new List<Team>();
            List<Project> UserProjects = new List<Project>();
            List<TaskManager.Models.Task> UserTasks = new List<TaskManager.Models.Task>();
        }
    }
}
