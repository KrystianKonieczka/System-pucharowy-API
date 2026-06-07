public class Bracket
{
    public int Id { get; set; }

    public int TournamentId { get; set; }

    public Tournament Tournament { get; set; }

    public List<Match> Matches { get; set; }
}