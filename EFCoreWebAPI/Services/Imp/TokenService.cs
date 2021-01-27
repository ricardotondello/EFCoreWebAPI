using System.Security.Cryptography;
using System;
using System.Text;
using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace EFCoreWebAPI.Services.Imp
{
    public class TokenService : ITokenService
    {
        private readonly TokenConfiguration _config;
        public TokenService(TokenConfiguration configuration)
        {
            _config = configuration;
        }
        public string GenerateAcessToken(IEnumerable<Claim> claims)
        {
           var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Secret));
           var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
           var options = new JwtSecurityToken(
               issuer: _config.Issuer,
               audience: _config.Audience,
               claims: claims,
               expires: DateTime.Now.AddMinutes(_config.Minutes),
               signingCredentials: signingCredentials
           );

           return new JwtSecurityTokenHandler()
            .WriteToken(options);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using(var rng = RandomNumberGenerator.Create()){
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            };
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameteres = new TokenValidationParameters 
            {
                ValidateAudience = false,
                ValidateIssuer=false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Secret)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken  securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameteres, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCulture)) throw new SecurityTokenException("Invalid token");
            
            return principal;
        }
    }
}