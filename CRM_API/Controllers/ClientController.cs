using CRM_API.Data;
using CRM_API.Data.Services;
using CRM_API.Models;
using CRM_API.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRM_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientDbServices clientDbServices;

        public ClientController(IClientDbServices clientDbServices)
        {
            this.clientDbServices = clientDbServices;
        }

        [HttpGet]
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var clientList = await this.clientDbServices.GetClientsFromStoredProcedure();
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

        [HttpGet("{Id}")]
        public async Task<IActionResult> OnGetByIdAsync([FromRoute] Guid Id)
        {
            try
            {
                var client = await this.clientDbServices.GetClientByIdFromStoredProcedure(Id);
                if (client.ClientId != Guid.Empty)
                {
                    return Ok(client);
                }
                ModelState.AddModelError(string.Empty, "Client not found.");
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Server Error: " + ex.Message);
                return StatusCode(500, ModelState);
            }
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAsync([FromBody] ClientDto client)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);  
                }

                if (client != null)
                {

                    /*await this.cl
                    await this.dbContext.SaveChangesAsync();*/
                    return Ok(client);
                }

                ModelState.AddModelError(string.Empty, "Client data is invalid.");
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Server Error: " + ex.Message);
                return StatusCode(500, ModelState);
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> OnPutAsync([FromRoute] Guid Id, [FromBody] ClientDto clientDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);  
                }

                var client = ""; //await this.dbContext.UpdateClient(Id,clientDto.ClientName);
                if (client != null)
                {
                    await this.dbContext.SaveChangesAsync();
                    return Ok(client);
                }

                ModelState.AddModelError(string.Empty, "Client not found.");
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Server Error: " + ex.Message);
                return StatusCode(500, ModelState);
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> OnDeleteAsync([FromRoute] Guid Id)
        {
            try
            {
                var client = await this.dbContext.Clients.FirstOrDefaultAsync(x => x.ClientId == Id);
                if (client != null)
                {
                    this.dbContext.Remove(client);
                    await this.dbContext.SaveChangesAsync();
                    return Ok("Client has been deleted.");
                }

                ModelState.AddModelError(string.Empty, "Client not found.");
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
