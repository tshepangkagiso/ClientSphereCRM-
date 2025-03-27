using CRM_API.Data;
using CRM_API.Data.Services.Interfaces;
using CRM_API.Models;
using CRM_API.Models.DTOs;
using CRM_API.Services;
using CRM_API.Services.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CRM_API.Controllers
{
  
    [Route("[controller]")]
    [ApiController]
    //[JwtAuthFilterAttribute]
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
        public IActionResult OnGetById([FromRoute] Guid Id)
        {
            try
            {
                var client = this.clientDbServices.GetClientByIdFromStoredProcedure(Id);
                if (client.ClientID != Guid.Empty)
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
        public async Task<IActionResult> OnPostAsync([FromBody] CreateClientDto client)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                if (client != null)
                {
                    
                    var title = Convert.ToInt32(client.TitleId);
                    var name = client.ClientName;
                    var surname = client.ClientSurname;
                    var email = client.ClientEmail;
                    var address = client.ClientAddress;
                    var contacts = client.ClientContactNumber;
                    byte[] photo = client.ClientProfilePicture;
                    var type = client.TypeId;
                    var username = client.LoginUsername;
                    var password = PasswordServices.EncryptPassword(client.LoginPassword);

                    await this.clientDbServices.CreateClient(title, name, surname, email??"", contacts, address, photo, type,username,password);
                    await this.clientDbServices.SaveChangesAsync();
                    return Ok("Client successfully created.");
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

        [HttpPut]
        public async Task<IActionResult> OnPutAsync([FromBody] UpdateClientDto clientDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);


                var client = this.clientDbServices.GetClientByIdFromStoredProcedure(clientDto.ClientID);
                if (client.ClientID != Guid.Empty)
                {
                    var title = Convert.ToInt32(clientDto.TitleId);
                    var name = clientDto.ClientName;
                    var surname = clientDto.ClientSurname;
                    var email = clientDto.ClientEmail;
                    var address = clientDto.ClientAddress;
                    var contacts = clientDto.ClientContactNumber;
                    byte[] photo = client.ClientProfilePicture;
                    var type = clientDto.TypeId;

                    await this.clientDbServices.UpdateClient(client.ClientID, title, name, surname, email ?? "", contacts, address, photo, type);
                    await this.clientDbServices.SaveChangesAsync();
                    return Ok("Client successfully updated");
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
                var client = this.clientDbServices.GetClientByIdFromStoredProcedure(Id);
                if (client.ClientID != Guid.Empty)
                {
                    await this.clientDbServices.DeleteClientById(client.ClientID);
                    await this.clientDbServices.SaveChangesAsync();
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
