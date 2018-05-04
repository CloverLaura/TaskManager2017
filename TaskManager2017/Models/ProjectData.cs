using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    public class ProjectData
    {
        static public List<Project> allProjects = new List<Project>();

        static int nextPID = 1;

        public void Add(Project project)
        {
            project.ProjectID = nextPID++;
            allProjects.Add(project);
        }

        public List<Project> ViewAllProjects()
        {
            return allProjects;
        }

        public Project GetByID(int id)
        {
            var project = allProjects.Find(p => p.ProjectID == id);
            return project;
        }

        public Project GetByName(string name)
        {
            var project = allProjects.Find(p => p.Name == name);
            return project;
        }

        static ProjectData()
        {
            
        }

        
    }
}
