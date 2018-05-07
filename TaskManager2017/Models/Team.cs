using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    public class Team
    {
        public int TeamID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        [NotMapped]
        public List<User> UsersInTeam { get; set; }
        [NotMapped]
        public List<Project> TeamProjects { get; set; }

        public Team()
        {
            UsersInTeam = new List<User>();
            TeamProjects = new List<Project>();
        }

        
    }
}
