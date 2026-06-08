public static class MapHomeClass
{
    public static IEndpointRouteBuilder MapHome(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async context =>
        {
        await context.Response.WriteAsync("""
            <html lang="pl">
                <head>
                    <meta charset="utf-8" />
                    <title>Strona główna</title>
                </head>

                <body>
                    <h1>Witaj 👋</h1>

                    <a href="/graphql">Przejdź do GraphQL</a>

                    <h3>A oto przykładowe zapytania i mutacje do bazy (Część z tych zapytań należy wykonywać w odpowiedniej kolejności):</h3>

                    <p>Pobranie turniejów</p>
                    <pre>
            query {
            tournaments {
                id
                name
            }
            }
                    </pre>

                    <p>Zarejestruj użytkownika W GraphQL (podmień dane bo to przykładowe konto):</p>
                    <pre>
            mutation {
            register(
            email:"test@test.pl"
            password:"123"
            firstName:"Jan"
            lastName:"Nowak"
            )
            }
                    </pre>

                    <p>Zaloguj się (podmień dane bo to przykładowe konto):</p>
                    <pre>
            mutation {
            login(
            email:"test@test.pl"
            password:"123"
            )
            }
                    </pre>

                    <p>Utwórz turniej:</p>
                    <pre>
            mutation {
            createTournament(
            name:"Turniej WSB"
            ){
            id
            name
            }
            }
                    </pre>

                    <p>Dodaj uczestnika:</p>
                    <pre>
            mutation {
            addParticipant(
            tournamentId:1
            userId:1
            )
            }
                    </pre>

                    <p>Sprawdź turnieje z uczestnikami:</p>
                    <pre>
            query {
            tournaments {
            id
            name
            participants {
                firstName
            }
            }
            }
                    </pre>
            
            <p>Utwórz drabinkę (Liczba uczestników meczu musi wynosić 2^n):</p>
                    <pre>
            mutation{
            generateBracket(
            tournamentId:1
            )
            }

                    </pre>

            <p>Sprawdź mecze z uczestnikami:</p>
                    <pre>
            query{
            matches{
            id
            round
            player1{
                firstName
            }
            player2{
                firstName
            }
            }
            }
                    </pre>
            

                </body>
            </html>
            """);
        });

        return app;
    }
}