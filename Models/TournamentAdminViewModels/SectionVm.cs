using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SquashBotWebCore.Models.SquashBot.Classes.Enums;
using SquashBotWebCore.Models.SquashBot.Classes.TournamentClasses;

namespace SquashBotWebCore.Models.TournamentAdminViewModels
{
    public class SectionVm
    {
        public SectionVm()
        {
            Teams = new List<Team>();
        }

        public SectionVm(Section section)
        {
            SectionId = section.SectionId;
            TournamentId = section.TournamentId;
            Name = section.Name;
            PlayersPerTeam = section.PlayersPerTeam;
            Par = section.Par;
            Gender = section.Gender;
            Teams = new List<Team>();
        }

        public Section Section()
        {
            Section s = new Section();
            s.SectionId = SectionId;
            s.TournamentId = TournamentId;
            s.Name = Name;
            s.PlayersPerTeam = PlayersPerTeam;
            s.Par = Par;
            s.Gender = Gender;
            return s;
        }

        public bool IsNew
        {
            get { return SectionId == 0; }
        }

        public int SectionId { get; set; }
        public int TournamentId { get; set; }
        public Tournament Tournament { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Players Per Team")]
        public int PlayersPerTeam { get; set; }

        [Required]
        [Display(Name = "Game Par")]
        public int Par { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public SectionGender Gender { get; set; }

        public virtual ICollection<Team> Teams { get; set; }
    }
}
