using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.Models
{
    public class User
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int UserID { get; set; }
        public bool LoggedOn { get; set; }

        public List<Team> UserTeams { get; private set; }
        public List<Project> UserProjects { get; private set; }
        public List<Task> UserTasks { get; private set; }

        public User()
        {
            UserTeams = new List<Team>();
            UserProjects = new List<Project>();
            UserTasks = new List<Task>();
        }


    }
}
