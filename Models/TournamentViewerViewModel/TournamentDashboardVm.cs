using System.Collections.Generic;
using SquashBotWebCore.Models.SquashBot.Classes.TournamentClasses;

namespace SquashBotWebCore.Models.TournamentViewerViewModel
{
    public class TournamentDashboardVm
    {
        public TournamentDashboardVm()
        {
            ActiveTournaments = new List<Tournament>();
            UpcomingTournaments = new List<Tournament>();
            PastTournaments = new List<Tournament>();
        }

        public virtual ICollection<Tournament> ActiveTournaments { get; set; }
        public virtual ICollection<Tournament> UpcomingTournaments { get; set; }
        public virtual ICollection<Tournament> PastTournaments { get; set; }

        //this method probably does not belong here
        public string FriendlyDateRange(Tournament tournament)
        {
            string range = "";
            range += tournament.StartDate.ToShortDateString();
            range += " - ";
            range += tournament.EndDate.ToShortDateString();
            return range;
        }
    }
}
