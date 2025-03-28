using CRM_EMPLOYEE_APP.Http.Interfaces;
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
        public async Task CreateClientAsync<T>(CreateClientDTO createClientDto)
        {
            var response = await this.GetHttpClient().PostAsJsonAsync("/Client",createClientDto);
            response.EnsureSuccessStatusCode();
        }

        //Update client
        public async Task UpdateClientAsync<T>(UpdateClientDTO updateClientDto)
        {
            var response = await this.GetHttpClient().PutAsJsonAsync("/Client", updateClientDto);
            response.EnsureSuccessStatusCode();
        }

        //Delete client
        public async Task DeleteClient(Guid id)
        {
            var response = await this.GetHttpClient().DeleteAsync($"/Client/delete/{id}");
            response.EnsureSuccessStatusCode();
        }

        //Get Image
        public async Task<Stream> GetClientImage(Guid id)
        {
            var response = await this.GetHttpClient().GetAsync($"/Client/image/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStreamAsync(); 
            }
            throw new HttpRequestException($"Failed to get image: {response.StatusCode}");
        }



        //Helper method to create httpclientfactor CreateClient()
        private HttpClient GetHttpClient()
        {
            return httpClientFactory.CreateClient(apiName);
        }
    }
}
