using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.ViewModels
{
    public class FindTasks_ViewTasksViewModel
    {
        public Project Project { get; set; }

        public int TaskID { get; set; }

        public List<Models.Task> Tasks { get; set; }

        public FindTasks_ViewTasksViewModel()
        {
            Tasks = new List<Models.Task>();
        }

    }
}
