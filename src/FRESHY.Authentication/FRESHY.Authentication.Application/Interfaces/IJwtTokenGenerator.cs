using System.Security.Claims;

namespace FRESHY.Authentication.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string Generator(List<Claim> authenticationClaims);
}