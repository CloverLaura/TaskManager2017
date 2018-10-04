using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager2017.Models
{
    public class TaskManager2017dbContext : DbContext
    {

        
        public TaskManager2017dbContext (DbContextOptions<TaskManager2017dbContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }

        
        

        public DbSet<Project> Project { get; set; }

        
         

       

        public DbSet<TaskManager.Models.Task> Task { get; set; }

        
        

        public DbSet<Team> Team { get; set; } 

        

    }

   
}

