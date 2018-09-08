using System;

namespace SquashBotWebCore.Models.SquashBot.Classes.TournamentClasses
{
    public class Fixture
    {
        public Fixture()
        {
            
        }

        public int FixtureId { get; set; }

        public int TeamAId { get; set; }
        public Team TeamA { get; set; }
        
        public int TeamBId { get; set; }
        public Team TeamB { get; set; }

        public Section Section { get; set; }
        public int SectionId { get; set; }

        public DateTime DateTime { get; set; }

        public TournamentSquashVenue TournamentSquashVenue { get; set; }
        public int TournamentSquashVenueId { get; set; }

        public string Court { get; set; }
    }
}
