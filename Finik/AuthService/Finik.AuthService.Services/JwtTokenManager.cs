using Finik.AuthService.Contracts;
using Finik.AuthService.Core;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Finik.AuthService.Services;

public class JwtTokenManager : IAuthManager
{
    private readonly AuthOptions jwtOptions;
    public JwtTokenManager(IOptions<AuthOptions> options)
    {
        jwtOptions = options.Value;
    }

    public string GenerateToken(UserDto user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtOptions.Key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Audience = jwtOptions.Audience,
            Issuer = jwtOptions.Issuer,
            Subject = new ClaimsIdentity(new[] 
            {
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.Email, user.Email.ToString()),
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
