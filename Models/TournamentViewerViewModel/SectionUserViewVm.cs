using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SquashBotWebCore.Models.SquashBot.Classes.TournamentClasses;

namespace SquashBotWebCore.Models.TournamentViewerViewModel
{
    public class SectionUserViewVm
    {
        public SectionUserViewVm(Tournament tournament)
        {
            Tournament = tournament;
            TournamentId = tournament.TournamentId;
            MensSections = new List<SectionDetailsVm>();
            WomensSections = new List<SectionDetailsVm>();
            MixedSections = new List<SectionDetailsVm>();
        }

        public int TournamentId { get; set; }
        public Tournament Tournament { get; set; }

        public List<SectionDetailsVm> MensSections { get; set; }
        public List<SectionDetailsVm> WomensSections { get; set; }
        public List<SectionDetailsVm> MixedSections { get; set; }

    }
}
