using System;

namespace SquashBotWebCore.Models.SquashBot.Classes.TournamentClasses
{
    public class TournamentAdministrator
    {
        public TournamentAdministrator()
        {

        }

        public int TournamentAdministratorId { get; set; }
        
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }
        public int TournamentId { get; set; }
        public virtual Tournament Tournament { get; set; }
    }
}
