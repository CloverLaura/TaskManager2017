using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [NotMapped]
        public string CreatedBy { get; set; }

        public int CreatedByInt { get; set; }

        [NotMapped]
        public int TaskCount { get; set; }
        [NotMapped]
        public string TeamP { get; set; }
        [NotMapped]
        public List<Task> Tasks { get; set; }

        public Project()
        {
            Tasks = new List<Task>();
        }
    }

    
}
