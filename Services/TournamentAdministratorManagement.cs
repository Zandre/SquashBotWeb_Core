using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using SquashBotWebCore.Data;
using SquashBotWebCore.Models;
using SquashBotWebCore.Models.SquashBot.Classes.TournamentClasses;

namespace SquashBotWebCore.Services
{
    public interface ITournamentAdministratorManagement
    {
        void AddTournamentAdministrator(Tournament tournament, ApplicationUser user);
        void RemoveTournamentAdministrator(Tournament tournament, ApplicationUser user);
        List<ApplicationUser> GetTournamentAdministrators(int tournamentId);
        List<ApplicationUser> GetAvailableUsers(int tournamentId, List<ApplicationUser> adminUsers);
    }

    public class TournamentAdministratorManagement : ITournamentAdministratorManagement
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TournamentAdministratorManagement(ApplicationDbContext context,
                                                    UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public void AddTournamentAdministrator(Tournament tournament, ApplicationUser user)
        {
            TournamentAdministrator tournamentAdmin = new TournamentAdministrator()
            {
                User = user,
                ApplicationUserId = new Guid(user.Id),
                Tournament = tournament
            };

            _context.Add(tournamentAdmin);
            _context.SaveChangesAsync();
        }

        public void RemoveTournamentAdministrator(Tournament tournament, ApplicationUser user)
        {
            TournamentAdministrator tournamentAdmin = _context.TournamentAdministrators
                                                                .SingleOrDefault(ta => ta.ApplicationUserId == new Guid(user.Id) &&
                                                                                            ta.TournamentId == tournament.TournamentId);

            _context.TournamentAdministrators.Remove(tournamentAdmin);
            _context.SaveChangesAsync();
        }

        public List<ApplicationUser> GetTournamentAdministrators(int tournamentId)
        {
            List<ApplicationUser> admins = _context.TournamentAdministrators
                                                    .Where(ta => ta.TournamentId == tournamentId)
                                                    .Select(a => a.User)
                                                    .ToList();
            return admins;
        }

        public List<ApplicationUser> GetAvailableUsers(int tournamentId, List<ApplicationUser> adminUsers)
        {
            List<ApplicationUser> available = _userManager.Users
                                                            .Where(u => adminUsers.All(u1 => u1.Id != u.Id))
                                                            .ToList();
            return available;
        }
    }
}
