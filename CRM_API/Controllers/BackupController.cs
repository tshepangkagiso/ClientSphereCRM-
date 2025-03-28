using CRM_API.Data.Services.Interfaces;
using CRM_API.Services.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [JwtAuthFilterAttribute]
    public class BackupController : ControllerBase
    {
        private readonly IClientDbServices clientDbServices;

        public BackupController(IClientDbServices clientDbServices)
        {
            this.clientDbServices = clientDbServices;
        }

        [HttpGet]
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var clientList = await this.clientDbServices.GetSoftDeleteStoredProcedure();
                if (clientList != null && clientList.Any())
                {
                    return Ok(clientList);
                }
                ModelState.AddModelError(string.Empty, "No clients found.");
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Server Error: " + ex.Message);
                return StatusCode(500, ModelState);
            }
        }
    }
}
