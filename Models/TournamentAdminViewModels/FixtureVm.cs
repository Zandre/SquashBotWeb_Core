using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using SquashBotWebCore.Models.SquashBot.Classes.TournamentClasses;

namespace SquashBotWebCore.Models.TournamentAdminViewModels
{
    public class FixtureVm
    {
        public FixtureVm()
        {
            AvailableVenues = new List<TournamentSquashVenue>();
            AvailableTeams = new List<Team>();
        }

        public FixtureVm(Fixture fixture)
        {
            FixtureId = fixture.FixtureId;
            TeamAId = fixture.TeamAId;
            TeamBId = fixture.TeamBId;
            SectionId = fixture.SectionId;
            DateTime = fixture.DateTime;
            Court = fixture.Court;
            TournamentSquashVenueId = fixture.TournamentSquashVenueId;
        }

        public Fixture Fixture()
        {
            Fixture fixture = new Fixture()
            {
                FixtureId = FixtureId,
                TeamAId = TeamAId,
                TeamBId = TeamBId,
                SectionId = SectionId,
                DateTime = DateTime,
                TournamentSquashVenueId = TournamentSquashVenueId,
                Court = Court
            };
            return fixture;
        }

        public bool IsNew => FixtureId == 0;

        public int FixtureId { get; set; }

        [Required]
        [Display(Name = "Team A")]
        public int TeamAId { get; set; }
        public Team TeamA { get; set; }

        [Required]
        [Display(Name = "Team B")]
        public int TeamBId { get; set; }
        public Team TeamB { get; set; }

        public Section Section { get; set; }
        public int SectionId { get; set; }

        [Required]
        [Display(Name = "Date & Time")]
        [DisplayFormat(DataFormatString = "{0:d MMMM yyyy HH:mm}")]
        public DateTime DateTime { get; set; }

        [Required]
        public string Court { get; set; }

        [Required]
        [Display(Name = "Venue")]
        public int TournamentSquashVenueId { get; set; }
        public TournamentSquashVenue TournamentSquashVenue { get; set; }        

        public List<TournamentSquashVenue> AvailableVenues { get; set; }
        public List<Team> AvailableTeams { get; set; }
    }
}
