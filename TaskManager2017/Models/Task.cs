using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    public class Task
    {
        public int TaskID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProjectID { get; set; }
        public bool Completed { get; set; }
        public bool IsTaken { get; set; }
        public string TakenBy { get; set; }
        public string CreatedBy { get; set; }

        public Task()
        {
            TakenBy = "";
        }
    }
}
