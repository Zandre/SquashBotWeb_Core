using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SquashBotWebCore.Models.SquashBot.Classes.TournamentClasses;

namespace SquashBotWebCore.Models.TournamentAdminViewModels
{
    public class TournamentAdministratorVm
    {
        public TournamentAdministratorVm(Tournament tournament)
        {
            Tournament = tournament;
            AdminUsers = new List<ApplicationUser>();
            AvailableUsers = new List<ApplicationUser>();
        }

        public List<ApplicationUser> AdminUsers { get; set; }
        public List<ApplicationUser> AvailableUsers { get; set; }
        public virtual Tournament Tournament { get; set; }
    }
}
