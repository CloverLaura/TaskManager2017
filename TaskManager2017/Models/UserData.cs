using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net;

namespace TaskManager.Models
{
    public class UserData
    {
        static public List<User> Users = new List<User>();
        

        static int nextId = 1;

        public void Add(User user)
        {
            user.UserID = nextId++;
            user.LoggedOn = false;
            Users.Add(user);
        }

        public List<User> AllUsersToList()
        {
            return Users.ToList();
        }

        public void AddTeam(User user, string team)
        {
            TeamData teamData = new TeamData();
            Team newTeam = teamData.FindByName(team);
            user.UserTeams.Add(newTeam);
        }

        public void AddProject(User user, Project project)
        {
            user.UserProjects.Add(project);
        }

        public void AddTask(User user, Task task)
        {
            user.UserTasks.Add(task);
        }

        public User GetById(int id)
        {
            var user = Users.Find(u => u.UserID == id);
            return user;
        }

        public User GetByEmail(string email)
        {
            User user = Users.Find(u => u.Email == email);
            return user;
        }

        public User SearchForUser(string username)
        {
            User user = Users.Find(u => u.Username == username);
            return user;
        }

        public Task FindByName(User user, string name)
        {
            Task task = new Task();
            task = user.UserTasks.Find(t => t.Name == name);
            return task;
        }

        public List<Team> GetTeamToList(User user)
        {
            List<Team> teams = new List<Team>();
            foreach(var team in TeamData.HardTeams.ToList())
            {
                teams.Add(team);
            }

            return teams;
        }

        public bool ValidateEmail(string email)
        {

            bool bVal = false;
            foreach(User user in Users)
            {
                if (user.Email == email)
                {
                    bVal = true;
                }
            }

            return bVal;
           
            
        }

        public bool IsValidPhone(string Phone)
        {
            try
            {
                if (string.IsNullOrEmpty(Phone))
                    return false;
                var r = new Regex(@"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$");
                return r.IsMatch(Phone);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsValidUsername(string username)
        {
            foreach(User user in Users)
            {
                if (username == user.Username)
                {
                    return false;
                }
            }
            return true;
        }

        

        static UserData()
        {

            User laura = new User
            {
                Username = "Clover",
                FirstName = "Laura",
                LastName = "Clover",
                Email = "laura.clover3@gmail.com",
                Password = "monkey",
                UserID = 33
            };
            TeamData teamData = new TeamData();
            laura.UserTeams.Add(teamData.FindByName("Night Stalkers"));
            Users.Add(laura);

            User john = new User
            {
                Username = "John",
                FirstName = "John",
                LastName = "Doe",
                Email = "JohnDoe@gmail.com",
                Password = "monkey",
                UserID = 3333
            };
            Users.Add(john);

            User jane = new User
            {
                Username = "Jane",
                FirstName = "Jane",
                LastName = "Doe",
                Email = "JaneDoe@gmail.com",
                Password = "monkey",
                UserID = 333
            };
            Users.Add(jane);
        }
        
        
    }
}
