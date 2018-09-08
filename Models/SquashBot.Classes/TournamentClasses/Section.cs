using System.Collections.Generic;
using SquashBotWebCore.Models.SquashBot.Classes.Enums;

namespace SquashBotWebCore.Models.SquashBot.Classes.TournamentClasses
{
    public class Section
    {
        public Section()
        {
            Teams = new List<Team>();
        }

        public int SectionId { get; set; }
        public int TournamentId { get; set; }
        public Tournament Tournament { get; set; }
        public string Name { get; set; }
        public int PlayersPerTeam { get; set; }
        public int Par { get; set; }
        public SectionGender Gender { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
    }
}
