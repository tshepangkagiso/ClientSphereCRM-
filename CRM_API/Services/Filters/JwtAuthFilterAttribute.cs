using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace CRM_API.Services.Filters
{
    public class JwtAuthFilterAttribute :Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // Check if the Authorization header exists
            if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!AuthServices.VerifyToken(token))
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }

}
