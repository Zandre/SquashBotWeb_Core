namespace SquashBotWebCore.Models.SquashBot.Classes.TournamentClasses
{
    public class Team
    {
        public Team()
        {

        }

        public int TeamId { get; set; }
        public Section Section { get; set; }
        public int SectionId { get; set; }
        public string Name { get; set; }
    }
}
