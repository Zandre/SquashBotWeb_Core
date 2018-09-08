using System.Collections.Generic;
using SquashBotWebCore.Models.SquashBot.Classes.TournamentClasses;

namespace SquashBotWebCore.Models.TournamentAdminViewModels
{
    public class TournamentSquashVenueVm
    {
        public TournamentSquashVenueVm(Tournament tournament)
        {
            Tournament = tournament;
            SquashVenues = new List<SquashVenue>();
            AvailableSquashVenues = new List<SquashVenue>();
        }

        public List<SquashVenue> SquashVenues { get; set; }
        public List<SquashVenue> AvailableSquashVenues { get; set; }
        public virtual Tournament Tournament { get; set; }
    }
}
