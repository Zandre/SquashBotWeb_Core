using System.Collections.Generic;

namespace SquashBotWebCore.Models.TournamentAdminViewModels
{
    public class FixtureIndexVm
    {
        public FixtureIndexVm()
        {
            FixtureVms = new List<FixtureVm>();
        }

        public SectionVm SectionVm { get; set; }
        public List<FixtureVm> FixtureVms { get; set; }
    }
}
