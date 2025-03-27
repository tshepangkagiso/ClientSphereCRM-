using DotNetEnv;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CRM_API.Services
{
    public class AuthServices
    {
        public static string CreateClientToken(Guid userID, DateTime expiresAt)
        {
            var key = Environment.GetEnvironmentVariable("SECRET_KEY");

            if (string.IsNullOrEmpty(key))
            {
                return string.Empty;
            }
            var secretKey = Encoding.ASCII.GetBytes(key);
            var claims = new List<Claim>
                {
                    new Claim("Client","true"),
                    new Claim("ID",$"{userID}")
                };

            var jwt = new JwtSecurityToken(signingCredentials:
                new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature),
                claims: claims,
                expires: expiresAt,
                notBefore: DateTime.UtcNow);

            return new JwtSecurityTokenHandler().WriteToken(jwt);

        }

        public static string CreateEmployeeToken(Guid userID, DateTime expiresAt)
        {
            var key = Environment.GetEnvironmentVariable("SECRET_KEY");

            if (string.IsNullOrEmpty(key))
            {
                return string.Empty;
            }
            var secretKey = Encoding.ASCII.GetBytes(key);

            var claims = new List<Claim>
            {
                new Claim("Employee","true"),
                new Claim("ID",$"{userID}")
            };

            var jwt = new JwtSecurityToken(signingCredentials:
                new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature),
                claims: claims,
                expires: expiresAt,
                notBefore: DateTime.UtcNow);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }


        public static bool VerifyToken(string token)
        {
            if (string.IsNullOrEmpty(token)) return false;

            var key = Environment.GetEnvironmentVariable("SECRET_KEY");
            if (string.IsNullOrEmpty(key)) return false;
            var secretKey = Encoding.ASCII.GetBytes(key);

            SecurityToken securityToken;
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateLifetime = true,
                    ValidateAudience =false,
                    ValidateIssuer =false,
                    ClockSkew = TimeSpan.Zero
                }, out securityToken);
            }
            catch (SecurityTokenException)
            {
                return false;
            }
            catch
            {
                throw;
            }
            return securityToken!=null;

        }
    }
}
