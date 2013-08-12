namespace Fritz.FFL.Data
{
    public class FantasyTeam
    {

        public FantasyTeam(int? id, string o)
        {
            Owner = o;
            Id = id;
        }

        public string Owner { get; private set; }

        public int? Id { get; private set; }

        public static FantasyTeam[] Teams = new[]
        {
            new FantasyTeam(null, "-- None --"),
            new FantasyTeam(1, "Team1"),
            new FantasyTeam(2, "Team2"),
            new FantasyTeam(3, "Team3"),
            new FantasyTeam(4, "Team4"),
            new FantasyTeam(5, "Team5"),
            new FantasyTeam(6, "Team6"),
            new FantasyTeam(7, "Team7"),
            new FantasyTeam(8, "Team8")
        };

    }

}