using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
.AddDbContext<AppDbContext>( x => x.UseSqlite("Data Source=db.db"));

builder.Services
.AddScoped<JwtService>();

builder.Services
.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer();

builder.Services
.AddAuthorization();

builder.Services
.AddGraphQLServer()
.AddAuthorization()
.AddQueryType<Query>()
.AddMutationType<Mutation>()
.AddFiltering()
.ModifyRequestOptions(x => x.IncludeExceptionDetails = true);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapHome();
app.MapGraphQL();
app.Run();