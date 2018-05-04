using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    public class TeamData
    {
        static public List<Team> HardTeams = new List<Team>();

        static int nextId = 1;


        public void Add(Team team)
        {
            team.TeamID = nextId++;
            HardTeams.Add(team);
        }

        public void AddProjectToTeam(Team team, Project project)
        {
            team.TeamProjects.Add(project);
        }

        public List<Team> TeamsToList()
        {
            return TeamData.HardTeams.ToList();
        }

        public Team FindByName(string name)
        {
            var team = HardTeams.Find(t => t.Name == name);
            return team;
        }


        static TeamData()
        {
            HardTeams.Add(new Team
            {
                Name = "Night Stalkers",
                Description = "Stalks the neighborhood for innocent voters.",
                CreatedBy = 33333
            });

            HardTeams.Add(new Team
            {
                Name = "Money Stealers",
                Description = "Collects money from unsuspecting do-gooders.",
                CreatedBy = 33333
            });
        }
    }
}
