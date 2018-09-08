using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SquashBotWebCore.Models.SquashBot.Classes.Enums;

namespace SquashBotWebCore.Models.SquashBot.Classes.TournamentClasses
{
    public class Tournament
    {
        public Tournament()
        {
            Sections = new List<Section>();
            TournamentAdministrators = new List<TournamentAdministrator>();
        }

        public int TournamentId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Section> Sections { get; set; }
        public SectionType SectionType { get; set; }

        public virtual ICollection<TournamentAdministrator> TournamentAdministrators { get; set; }

        //I'm annotating DateTime attributes for now because it is being used in Tournaments/Index,
        //remove this when Index can switch to a viewModel and not use this class anymore
        [DisplayFormat(DataFormatString = "{0:d MMMM yyyy}")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:d MMMM yyyy}")]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }                  
    }
}
