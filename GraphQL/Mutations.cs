using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

public class Mutation
{
    public async Task<string> Register(
        string email,
        string password,
        string firstName,
        string lastName,
        [Service] AppDbContext db
    )
    {
        var user = new User
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
            };

        db.Users.Add(user);

        await db.SaveChangesAsync();

        return "OK";
    }

    public async Task<string> Login(
        string email,
        string password,
        [Service] AppDbContext db,
        [Service] JwtService jwt
    )
    {
        var user = db.Users
            .First(x => x.Email == email);

        if (!BCrypt.Net.BCrypt
                .Verify(
                    password,
                    user.PasswordHash
                )
        )
            throw new Exception();

        return jwt.Generate(user);
    }
    public async Task<Tournament> CreateTournament(
        string name,
        [Service] AppDbContext db
    )
    {
        var t =
            new Tournament
            {
                Name=name,
                StartDate=DateTime.UtcNow,
                Status="Created"
            };

        db.Tournaments.Add(t);

        await db.SaveChangesAsync();

        return t;
    }

    public async Task<string> AddParticipant(
        int tournamentId,
        int userId,
        [Service] AppDbContext db
    )
    {
        var tournament =
            await db.Tournaments
                .Include(t => t.Participants)
                .FirstAsync(
                    t => t.Id == tournamentId
                );

        var user =
            await db.Users
                .FirstAsync(
                    u => u.Id == userId
                );

        tournament.Participants.Add(user);

        await db.SaveChangesAsync();

        return "Added";
    }

    public async Task<string> GenerateBracket(
        int tournamentId,
        [Service] AppDbContext db
    )
    {
        var tournament =
            await db.Tournaments
                .Include(x => x.Participants)
                .FirstAsync(
                    x => x.Id == tournamentId
                );

        var players =
            tournament.Participants
                .OrderBy(
                    x => Guid.NewGuid()
                )
                .ToList();

        var count =
            players.Count;

        bool powerOfTwo =
            count > 0
            &&
            (count & (count - 1))
            == 0;

        if (!powerOfTwo)
        {
            throw new Exception(
                "Liczba uczestników musi być 2^n"
            );
        }

        for (
            int i = 0;
            i < count;
            i += 2
        )
        {
            db.Matches.Add(
                new Match
                {
                    TournamentId =
                        tournament.Id,

                    Round = 1,

                    Player1Id =
                        players[i].Id,

                    Player2Id =
                        players[i + 1].Id
                }
            );
        }

        tournament.Status =
            "Running";

        await db.SaveChangesAsync();

        return "Bracket generated";
    }
}