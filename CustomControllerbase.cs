using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InspectionService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace InspectionService.Controllers
{
    public partial class CustomControllerbase : ControllerBase
    {
        protected User GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new User
                {
                    Username = userClaims.FirstOrDefault(x => x.Type == "UserName")?.Value,
                    id = int.Parse(userClaims.FirstOrDefault(x => x.Type == "UserId")?.Value),
                    DisplayName = userClaims.FirstOrDefault(x => x.Type == "DisplayName")?.Value
                };
            }
            return null;
        }

        protected ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token, IConfiguration _configuration)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
}