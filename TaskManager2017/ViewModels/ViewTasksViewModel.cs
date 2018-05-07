using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.ViewModels
{
    public class ViewTasksViewModel
    {

        public User User { get; set; }

        public List<Models.Task> Tasks { get; set; }

        public int TaskID { get; set; }

        /*public Project Project { get; set; }

        public String CreatedBy { get; set; }*/




       
            

    }
}
