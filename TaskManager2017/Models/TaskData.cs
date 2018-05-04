using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    public class TaskData
    {
        static public List<Models.Task> allTasks = new List<Models.Task>();

        static int nextTID = 1;

        public void Add(Models.Task task)
        {
            task.TaskID = nextTID++;
            task.Completed = false;
            task.IsTaken = false;
            allTasks.Add(task);
        }

        public void AddTask(Project project, Task task)
        {
            project.Tasks.Add(task);
        }

        public Task GetByID(int id)
        {
            var task = allTasks.Find(t => t.TaskID == id);
            return task;
        }

        public Task GetByName(string name)
        {
            var task = allTasks.Find(t => t.Name == name);
            return task;
        }

        static TaskData()
        {
            ProjectData projectdata = new ProjectData();
            TaskData taskData = new TaskData();
            Project project1 = projectdata.GetByName("Test Project 1");
            //Task task1 = new Task();
        }
        
    }
}
