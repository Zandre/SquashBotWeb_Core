using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SquashBotWebCore.Models.SquashBot.Classes.TournamentClasses
{
    public class TournamentSquashVenue
    {
        public TournamentSquashVenue()
        {
            
        }

        public int TournamentSquashVenueId { get; set; }
        public Tournament Tournament { get; set; }
        public int TournamentId { get; set; }
        public SquashVenue SquashVenue { get; set; }
        public int SquashVenueId { get; set; }
    }
}
