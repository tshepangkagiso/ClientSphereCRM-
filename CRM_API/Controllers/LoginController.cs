using CRM_API.Data;
using CRM_API.Data.Services;
using CRM_API.Data.Services.Interfaces;
using CRM_API.Models.DTOs;
using CRM_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginDbServices loginDbServices;

        public LoginController(ILoginDbServices loginDbServices)
        {
            this.loginDbServices = loginDbServices;
        }

        [HttpPost("employee")]
        public async Task<IActionResult> OnPostEmployee([FromBody] LoginDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var employee = await this.loginDbServices.EmployeeLogin(loginDto);

                if (employee != null)
                {
                    var expires_At =DateTime.UtcNow.AddMinutes(30);
                    
                    var response = new
                    {
                        ID = employee.EmployeeID,
                        Token = AuthServices.CreateEmployeeToken(employee.EmployeeID, expires_At),
                        ExpireAt = expires_At
                    };


                    return Ok(response);
                }


                return StatusCode(400, "Failed to login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Server Error: " + ex.Message);
                return StatusCode(500, ModelState);
            }
        }

        [HttpPost("client")]
        public async Task<IActionResult> OnPostClient([FromBody] LoginDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var client = await this.loginDbServices.ClientLogin(loginDto);
                if (client != null)
                {
                    var expires_At = DateTime.UtcNow.AddMinutes(30);
                    var response = new
                    {
                        ID = client.ClientID,
                        Token = AuthServices.CreateClientToken(client.ClientID, expires_At),
                        ExpireAt = expires_At
                    };
                    return Ok(response);
                }

                return StatusCode(400, "Failed to login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Server Error: " + ex.Message);
                return StatusCode(500, ModelState);
            }
        }
    }

}
