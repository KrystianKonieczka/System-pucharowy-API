using System.Security.Claims;
using HotChocolate.Authorization;
using Microsoft.EntityFrameworkCore;

public class Query
{
    public IQueryable<Tournament> GetTournaments(
        [Service] AppDbContext db
    ) 
    {
        return db.Tournaments
            .Include(t => t.Participants);
    }

    [Authorize]
    public IQueryable<Match> MyMatches(
        ClaimsPrincipal user,
        [Service] AppDbContext db
    )
    {
        var id = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!);

        return db.Matches
            .Include(x => x.Player1)
            .Include(x => x.Player2)
            .Include(x => x.Winner)
            .Where(m => m.Player1Id == id || m.Player2Id == id);
    }
    public IQueryable<User> GetUsers(
        [Service] AppDbContext db
    )
    {
        return db.Users;
    }

    public IQueryable<Match> GetMatches(
        [Service] AppDbContext db
    )
    {
        return db.Matches
            .Include(x => x.Player1)
            .Include(x => x.Player2)
            .Include(x => x.Winner);
    }
}