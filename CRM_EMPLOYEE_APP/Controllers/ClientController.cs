﻿using CRM_EMPLOYEE_APP.Http.Interfaces;
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
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return RedirectToAction("/Home/Error");
            }
        }



        [HttpPost("{id:guid}")]
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
                    return RedirectToAction("/Index");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return RedirectToAction("/Home/Error");
            }

        }


    }
}
