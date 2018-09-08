using System.Collections.Generic;
using System.Linq;
using SquashBotWebCore.Data;
using SquashBotWebCore.Models.SquashBot.Classes.TournamentClasses;

namespace SquashBotWebCore.Services
{
    public interface ITeamManagement
    {
        List<Team> GetTeamsForSection(int sectionId);
        Team GetTeamById(int id);
        void UpdateTeam(Team team);
        int DeleteTeam(int id);
    }

    public class TeamManagement : ITeamManagement
    {
        private readonly ApplicationDbContext _context;

        public TeamManagement(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Team> GetTeamsForSection(int sectionId)
        {
            List<Team> teams = _context.Team
                                        .Where(t => t.SectionId == sectionId)
                                        .ToList();

            return teams;
        }

        public Team GetTeamById(int id)
        {
            Team team = _context.Team.SingleOrDefault(t => t.TeamId == id);
            return team;
        }

        public void UpdateTeam(Team team)
        {
            if (team.TeamId == 0)
            {
                CreateNewTeam(team);
            }
            else
            {
                SaveExistingTeam(team);
            }
        }

        private void CreateNewTeam(Team team)
        {
            _context.Add(team);
            _context.SaveChanges();
        }

        private void SaveExistingTeam(Team team)
        {
            var _team = GetTeamById(team.TeamId);

            _team.Name = team.Name;

            _context.SaveChanges();
        }

        public int DeleteTeam(int id)
        {
            var team = GetTeamById(id);
            if (team != null)
            {
                int sectionId = team.SectionId;
                _context.Team.Remove(team);
                _context.SaveChanges();

                return sectionId;
            }

            return 0;
        }
    }
}
