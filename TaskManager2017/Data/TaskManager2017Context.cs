using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models;
using TaskManager2017.Models;

namespace TaskManager2017.Models
{
    public class TaskManager2017Context : DbContext
    {
        public TaskManager2017Context (DbContextOptions<TaskManager2017Context> options)
            : base(options)
        {
        }

        /*public TaskManager2017Context()
        {

        }*/

        public DbSet<TaskManager.Models.User> User { get; set; }

        /*public TaskManager2017Context()
        {

        }*/

        public DbSet<TaskManager.Models.Project> Project { get; set; }

        /*public TaskManager2017Context()
        {

        }*/

        public DbSet<TaskManager.Models.Task> Task { get; set; }

        /*public TaskManager2017Context()
        {

        }*/

        public DbSet<TaskManager.Models.Team> Team { get; set; }

        /*public TaskManager2017Context()
        {

        }*/

        //public DbSet<TaskManager2017.Models.UserTeam> UserTeam { get; set; }
    }
}
