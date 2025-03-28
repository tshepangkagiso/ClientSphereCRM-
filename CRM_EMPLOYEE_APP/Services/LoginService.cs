using CRM_EMPLOYEE_APP.Models;
using CRM_EMPLOYEE_APP.Models.DTOs;
using System.Security.Claims;

namespace CRM_EMPLOYEE_APP.Services
{
    public class LoginService
    {
        public static ClaimsPrincipal SignInEmployee(string name, LoginResponse response)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, "Employee"),
                new Claim(ClaimTypes.Name,$"{name}"),
                new Claim("Id",$"{response.Id}")
            };

            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
            return claimsPrincipal;
        }
    }
}
