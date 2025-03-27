using CRM_EMPLOYEE_APP.Http.Interfaces;
using CRM_EMPLOYEE_APP.Models;
using CRM_EMPLOYEE_APP.Models.DTOs;

namespace CRM_EMPLOYEE_APP.Http
{
    public class ClientWebExecutor : IClientWebExecutor
    {
        private const string apiName = "CRM_API";
        private readonly IHttpClientFactory httpClientFactory;

        public ClientWebExecutor(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        //Get all client
        public async Task<T?> GetAllClientsAsync<T>()
        {
            return await this.GetHttpClient().GetFromJsonAsync<T>("/Client");
        }

        //Get client by id
        public async Task<T?> GetClientByIdAsync<T>(Guid id)
        {
            return await this.GetHttpClient().GetFromJsonAsync<T>($"/Client/{id}");
        }

        //Create client
        public async Task<T?> CreateClientAsync<T>(CreateClientDTO createClientDto)
        {
            var response = await this.GetHttpClient().PostAsJsonAsync("/Client",createClientDto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        //Update client
        public async Task UpdateClientAsync<T>(Client client)
        {
            var response = await this.GetHttpClient().PutAsJsonAsync("/Client", client);
            response.EnsureSuccessStatusCode();
        }

        //Delete client
        public async Task DeleteClient(Guid id)
        {
            var response = await this.GetHttpClient().DeleteAsync($"/Client/{id}");
            response.EnsureSuccessStatusCode();
        }

        //Helper method to create httpclientfactor CreateClient()
        private HttpClient GetHttpClient()
        {
            return httpClientFactory.CreateClient(apiName);
        }
    }
}
