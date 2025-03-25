using CRM_API.Data;
using CRM_API.Data.Services;
using CRM_API.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginEmployeeController : ControllerBase
    {
        private readonly LoginDbServices loginDbServices;

        public LoginEmployeeController(LoginDbServices loginDbServices)
        {
            this.loginDbServices = loginDbServices;
        }

        [HttpPost]
        public async Task<IActionResult> OnPost(LoginDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var employee = await this.loginDbServices.EmployeeLogin(loginDto);
                if (employee != null)
                {
                    return Ok(employee);
                }
                return StatusCode(400, "Failed to login");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
