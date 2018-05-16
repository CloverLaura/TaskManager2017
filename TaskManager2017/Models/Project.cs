using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    public class Project
    {
        public int ProjectID { get; set; }

        [Required (ErrorMessage="You must enter a name for your project")]
        public string Name { get; set; }

        [Required (ErrorMessage ="You must enter a description for your project")]
        public string Description { get; set; }

        public string CreatedBy { get; set; }
        public int TaskCount { get; set; }
        public string TeamP { get; set; }

        public List<Task> Tasks { get; set; }

        public Project()
        {
            Tasks = new List<Task>();
        }
    }

    
}
