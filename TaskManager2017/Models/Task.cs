using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    public class Task
    {
        public int TaskID { get; set; }

        [Required (ErrorMessage ="You must enter a name for your task")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Project { get; set; }
        public bool Completed { get; set; }
        public bool IsTaken { get; set; }
        public string TakenBy { get; set; }
        public int CreatedByInt { get; set; }
        public int ProjectID { get; set; }

        public Task()
        {
            
        }
    }
}
