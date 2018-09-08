using System;
using System.Collections.Generic;
using System.Linq;
using SquashBotWebCore.Data;
using SquashBotWebCore.Models;
using SquashBotWebCore.Models.SquashBot.Classes.TournamentClasses;

namespace SquashBotWebCore.Services
{
    public interface ITournamentManagement
    {
        void UpdateTournament(Tournament tournament, ApplicationUser user);
        void DeleteTournament(int id);
        List<Tournament> GetTournamentsForAdminUser(ApplicationUser user);
        List<Tournament> GetActiveTournaments();
        List<Tournament> GetUpcomingTournaments();
        List<Tournament> GetPastTournaments();
        Tournament GetTournamentById(int id);
        Tournament GetTournamentBySectionId(int sectionId);

        void AddTournamentAdministrator(int tournamentId, ApplicationUser user);
        void RemoveTournamentAdministrator(int tournamentId, ApplicationUser user);
    }

    public class TournamentManagement : ITournamentManagement
    {
        private readonly ApplicationDbContext _context;
        private readonly ITournamentAdministratorManagement _tournamentAdministratorManagement;
        private readonly ISectionManagement _sectionManagement;

        public TournamentManagement(ApplicationDbContext context,
                                    ITournamentAdministratorManagement tournamentAdministratorManagement,
                                    ISectionManagement sectionManagement)
        {
            _context = context;
            _tournamentAdministratorManagement = tournamentAdministratorManagement;
            _sectionManagement = sectionManagement;
        }

        public void UpdateTournament(Tournament tournament, ApplicationUser user)
        {
            if (tournament.TournamentId == 0)
            {
                CreateNewTournament(tournament, user);
            }
            else
            {
                SaveExistingTournament(tournament);
            }
        }

        private void SaveExistingTournament(Tournament tournament)
        {
            var _tournament = GetTournamentById(tournament.TournamentId);

            _tournament.Name = tournament.Name;
            _tournament.SectionType = tournament.SectionType;
            _tournament.StartDate = tournament.StartDate;
            _tournament.EndDate = tournament.EndDate;

            _context.SaveChanges();
        }

        private void CreateNewTournament(Tournament tournament, ApplicationUser user)
        {
            _context.Add(tournament);
            _context.SaveChangesAsync();

            _tournamentAdministratorManagement.AddTournamentAdministrator(tournament, user);
        }

        public void DeleteTournament(int id)
        {
            var tournament = GetTournamentById(id);
            if (tournament != null)
            {
                _context.Tournament.Remove(tournament);
                _context.SaveChanges();
            }
        }

        public List<Tournament> GetTournamentsForAdminUser(ApplicationUser user)
        {
            List<Tournament> tournaments = _context.TournamentAdministrators
                                                            .Where(ta => ta.User.Id == user.Id)
                                                            .Select(tAdmin => tAdmin.Tournament)
                                                            .OrderBy(t => t.Name)
                                                            .ToList();

            return tournaments;
        }

        public List<Tournament> GetActiveTournaments()
        {
            List<Tournament> tournaments = _context.Tournament
                                                    .Where(t => t.StartDate <= DateTime.Now && 
                                                                t.EndDate >= DateTime.Now)
                                                    .OrderBy(t => t.Name)
                                                    .ToList();
            return tournaments;
        }

        public List<Tournament> GetUpcomingTournaments()
        {
            List<Tournament> tournaments = _context.Tournament
                                                    .Where(t => t.StartDate > DateTime.Now)
                                                    .OrderBy(t => t.Name)
                                                    .ToList();
            return tournaments;
        }

        public List<Tournament> GetPastTournaments()
        {
            List<Tournament> tournaments = _context.Tournament
                                                    .Where(t => t.EndDate < DateTime.Now)
                                                    .OrderBy(t => t.Name)
                                                    .ToList();
            return tournaments;
        }

        public Tournament GetTournamentById(int id)
        {
            Tournament tournament = _context.Tournament.SingleOrDefault(t => t.TournamentId == id);
            return tournament;
        }

        public Tournament GetTournamentBySectionId(int sectionId)
        {
            Section section = _sectionManagement.GetSectionById(sectionId);
            Tournament tournament = _context.Tournament.SingleOrDefault(t => t.TournamentId == section.TournamentId);
            return tournament;
        }

        public void AddTournamentAdministrator(int tournamentId, ApplicationUser user)
        {
            Tournament tournament = GetTournamentById(tournamentId);
            _tournamentAdministratorManagement.AddTournamentAdministrator(tournament, user);
        }

        public void RemoveTournamentAdministrator(int tournamentId, ApplicationUser user)
        {
            Tournament tournament = GetTournamentById(tournamentId);
            _tournamentAdministratorManagement.RemoveTournamentAdministrator(tournament, user);
        }
    }
}
