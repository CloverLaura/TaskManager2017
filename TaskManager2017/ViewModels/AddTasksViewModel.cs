using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.ViewModels
{
    public class AddTasksViewModel
    {
        [Required (ErrorMessage = "You must have at least one task in your project")]
        [Display (Name ="Task name:")]
        public string Name { get; set; }

        [Display (Name = "Description of task (optional):")]
        public string Description { get; set; }


        
    }

    
}
