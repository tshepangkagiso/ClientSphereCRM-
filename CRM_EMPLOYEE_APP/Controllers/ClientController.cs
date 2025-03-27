using CRM_EMPLOYEE_APP.Http.Interfaces;
using CRM_EMPLOYEE_APP.Models;
using CRM_EMPLOYEE_APP.Models.DTOs;
using CRM_EMPLOYEE_APP.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRM_EMPLOYEE_APP.Controllers
{
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
                return RedirectToAction("/Home/Error");
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
                return View(client);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return RedirectToAction("/Home/Error");
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
                if(!ModelState.IsValid) return BadRequest(ModelState);
                CreateClientDTO createClient = new CreateClientDTO();

                createClient.TitleId = clientDto.TitleId;
                createClient.ClientName = clientDto.ClientName;
                createClient.ClientSurname = clientDto.ClientSurname;
                createClient.ClientContactNumber = clientDto.ClientContactNumber;
                createClient.ClientEmail = clientDto.ClientEmail;
                createClient.ClientAddress = clientDto.ClientAddress;
                createClient.TypeId = clientDto.TypeId;
                createClient.LoginUsername = clientDto.LoginUsername;
                createClient.LoginPassword = clientDto.LoginPassword;

                Stream content = clientDto.ClientProfilePicture.OpenReadStream();

                createClient.ClientProfilePicture = await ImageServices.ProcessImage(content);

                var response = await this.webExecutor.CreateClientAsync<CreateClientDTO>(createClient);

                if(response == null)
                {
                    return View(response);
                }
                return RedirectToAction("/Index");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return RedirectToAction("/Home/Error");
            }

        }
    }
}
