using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SquashBotWebCore.Models.SquashBot.Classes.TournamentClasses;

namespace SquashBotWebCore.Models.TournamentViewerViewModel
{
    public class SectionDetailsVm
    {
        public SectionDetailsVm(Section section)
        {
            Section = section;
            SectionId = section.SectionId;
            Teams = new List<Team>();
            Fixtures = new List<Fixture>();
        }

        public Section Section { get; set; }
        public int SectionId { get; set; }

        public List<Team> Teams { get; set; }
        public List<Fixture> Fixtures { get; set; }
    }
}
