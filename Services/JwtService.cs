using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class JwtService
{
    public string Generate(User user)
    {
        var key =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    "SUPER_SECRET_KEY_123456789_ABCDEFG"
                ));

        var creds =
            new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

        var token =
            new JwtSecurityToken(
                claims:
                [
                    new Claim(
                        ClaimTypes.NameIdentifier,
                        user.Id.ToString()
                    )
                ],
                expires:
                    DateTime.UtcNow.AddHours(12),
                signingCredentials:
                    creds
            );

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}