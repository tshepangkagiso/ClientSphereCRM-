using CRM_EMPLOYEE_APP.Http.Interfaces;
using CRM_EMPLOYEE_APP.Models;
using CRM_EMPLOYEE_APP.Models.DTOs;
using CRM_EMPLOYEE_APP.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Text.Json;

namespace CRM_EMPLOYEE_APP.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        private readonly IClientWebExecutor webExecutor;

        public ClientController(IClientWebExecutor webExecutor)
        {
            this.webExecutor = webExecutor;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var clients = await webExecutor.GetAllClientsAsync<List<Client>>();
                if(clients == null)
                {
                    return NotFound();
                }
                return View(clients);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //return RedirectToAction("/Home/Error");
                return RedirectToAction("Error", "Home");
            }
            
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            try
            {
                var client = await webExecutor.GetClientByIdAsync<Client>(id);
                if (client == null)
                {
                    return NotFound();
                }
                /*var photo = this.File(client.ClientProfilePicture, "image/jpeg");
                client.ClientProfilePicture = photo;*/
                return View(client);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //return RedirectToAction("/Home/Error");
                return RedirectToAction("Error", "Home");
            }

        }

        [HttpGet]
        public IActionResult CreateClient()
        {
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateClientForm clientDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                CreateClientDTO createClient = new CreateClientDTO
                {
                    TitleId = clientDto.TitleId,
                    ClientName = clientDto.ClientName,
                    ClientSurname = clientDto.ClientSurname,
                    ClientContactNumber = clientDto.ClientContactNumber,
                    ClientEmail = clientDto.ClientEmail,
                    ClientAddress = clientDto.ClientAddress,
                    TypeId = clientDto.TypeId,
                    LoginUsername = clientDto.LoginUsername,
                    LoginPassword = clientDto.LoginPassword
                };

                Stream content = clientDto.ClientProfilePicture.OpenReadStream();
                createClient.ClientProfilePicture = await ImageServices.ProcessImage(content);

                await this.webExecutor.CreateClientAsync<CreateClientDTO>(createClient);

                return RedirectToAction("Index","Client");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //return RedirectToAction("/Home/Error");
                return RedirectToAction("Error", "Home");
            }
        }



        public async Task<IActionResult> Update([FromForm] UpdateClientForm clientDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var dbClient = await webExecutor.GetClientByIdAsync<Client>(clientDto.ClientID);

                if (dbClient == null)
                    return BadRequest(clientDto);

                UpdateClientDTO client = new UpdateClientDTO
                {
                    ClientID = dbClient.ClientID,
                    TitleId = clientDto.TitleId,
                    ClientName = clientDto.ClientName,
                    ClientSurname = clientDto.ClientSurname,
                    ClientContactNumber = clientDto.ClientContactNumber,
                    ClientEmail = clientDto.ClientEmail,
                    ClientAddress = clientDto.ClientAddress,
                    TypeId = clientDto.TypeId,

                };

                if (clientDto.ClientProfilePicture != null) 
                {
                    Stream content = clientDto.ClientProfilePicture.OpenReadStream();
                    client.ClientProfilePicture = await ImageServices.ProcessImage(content);
                }
                else
                {
                    client.ClientProfilePicture = dbClient.ClientProfilePicture;
                }

                await webExecutor.UpdateClientAsync<UpdateClientDTO>(client);
                return RedirectToAction("Index","Client");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //return RedirectToAction("/Home/Error");
                return RedirectToAction("Error", "Home");
            }
        }


        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                var client = await webExecutor.GetClientByIdAsync<Client>(id);
                if (client == null)
                {
                    return NotFound();
                }
                else
                {
                    await webExecutor.DeleteClient(id);
                    return RedirectToAction("Index","Client");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //return RedirectToAction("/Home/Error");
                return RedirectToAction("Error", "Home");
            }

        }

        public async Task<IActionResult> Image([FromRoute] Guid id) => this.ResultImage(await this.webExecutor.GetClientImage(id));



        private IActionResult ResultImage(Stream image)
        {
            var header = Response.GetTypedHeaders();
            header.CacheControl = new CacheControlHeaderValue
            {
                Public = true,
                MaxAge = TimeSpan.FromDays(1) // 30days is ideal
            };

            header.Expires = new DateTimeOffset(DateTime.UtcNow.AddDays(1));
            return this.File(image, "image/jpeg");
        }
    }
}
