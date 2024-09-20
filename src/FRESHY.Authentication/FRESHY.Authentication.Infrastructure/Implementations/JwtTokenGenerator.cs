using FRESHY.Authentication.Application.Interfaces;
using FRESHY.Authentication.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FRESHY.Authentication.Infrastructure.Implementations;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;

    public JwtTokenGenerator(IOptions<JwtSettings> options)
    {
        _jwtSettings = options.Value;
    }

    public string Generator(List<Claim> authenticationClaims)
    {
        var signinCredentials = new SigningCredentials
        (
            new SymmetricSecurityKey
            (
                Encoding.UTF8.GetBytes(_jwtSettings.Secret!)
            ),
            SecurityAlgorithms.HmacSha256
        );

        var secureToken = new JwtSecurityToken
        (
            claims: authenticationClaims,
            signingCredentials: signinCredentials,
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: DateTime.UtcNow.AddDays(_jwtSettings.ExpiryDays)
        );

        return new JwtSecurityTokenHandler().WriteToken(secureToken);
    }
}