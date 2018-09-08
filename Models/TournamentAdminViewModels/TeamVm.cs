using System.ComponentModel.DataAnnotations;
using SquashBotWebCore.Models.SquashBot.Classes.TournamentClasses;

namespace SquashBotWebCore.Models.TournamentAdminViewModels
{
    public class TeamVm
    {
        public TeamVm()
        {

        }

        public TeamVm(Team team)
        {
            TeamId = team.TeamId;
            SectionId = team.SectionId;
            Name = team.Name;
        }

        public Team Team()
        {
            Team t = new Team();
            t.TeamId = TeamId;
            t.SectionId = SectionId;
            t.Name = Name;
            return t;
        }

        public bool IsNew => TeamId == 0;

        public int TeamId { get; set; }
        public Section Section { get; set; }
        public int SectionId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}
