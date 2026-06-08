public class Tournament
{
    public int Id { get; set; }

    public string Name { get; set; }

    public DateTime StartDate { get; set; }

    public string Status { get; set; }

    public List<User> Participants { get; set; }
        = new();
    
        public List<Match> Matches { get; set; }
        = new();
}