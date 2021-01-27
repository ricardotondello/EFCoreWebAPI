using System.Collections.Generic;
using System.Security.Claims;

namespace EFCoreWebAPI.Services
{
    public interface ITokenService
    {
         string GenerateAcessToken(IEnumerable<Claim> claims);
         string GenerateRefreshToken();
         ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}