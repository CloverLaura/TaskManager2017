using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    public class Project
    {
        public int ProjectID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }

        public List<Task> Tasks { get; set; }

        public Project()
        {
            Tasks = new List<Task>();
        }
    }

    
}
