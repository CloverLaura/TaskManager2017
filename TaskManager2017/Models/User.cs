using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.Models
{
    public class User
    {
        [Required (ErrorMessage ="You must enter a username")]
        public string Username { get; set; }

        [Required (ErrorMessage ="You must enter an email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required (ErrorMessage= "You must enter a password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [NotMapped]
        //[Required (ErrorMessage ="You must verify your password")]
        [DataType(DataType.Password)]
        [Display(Name ="Verify Password")]
        public string ConfirmPassword { get; set; }

        public int UserID { get; set; }

        public bool LoggedOn { get; set; }
        [NotMapped]
        public List<Team> UserTeams { get; private set; }
        [NotMapped]
        public List<Project> UserProjects { get; private set; }
        [NotMapped]
        public List<Task> UserTasks { get; private set; }

        public User()
        {
            UserTeams = new List<Team>();
            UserProjects = new List<Project>();
            UserTasks = new List<Task>();
        }


    }
}
