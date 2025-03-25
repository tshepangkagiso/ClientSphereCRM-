using CRM_API.Data;
using CRM_API.Data.Services;
using CRM_API.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginClientController : ControllerBase
    {
        private readonly LoginDbServices loginDbServices;

        public LoginClientController(LoginDbServices loginDbServices)
        {
            this.loginDbServices = loginDbServices;
        }

        [HttpPost]
        public async Task<IActionResult> OnPost(LoginDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var client = await this.loginDbServices.ClientLogin(loginDto);
                if (client != null)
                {
                    return Ok(client);
                }
                return StatusCode(400, "Failed to login");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
