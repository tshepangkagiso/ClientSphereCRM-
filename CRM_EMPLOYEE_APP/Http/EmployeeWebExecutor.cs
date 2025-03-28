using CRM_EMPLOYEE_APP.Http.Interfaces;
using CRM_EMPLOYEE_APP.Models;

namespace CRM_EMPLOYEE_APP.Http
{
    public class EmployeeWebExecutor : IEmployeeWebExecutor
    {
        private const string apiName = "CRM_API";
        private readonly IHttpClientFactory httpClientFactory;
        public EmployeeWebExecutor(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        //Employee Login
        public async Task<T?> Login<T>(AuthUser user)
        {
            var requestBody = new
            {
                loginUsername = user.UserName,
                loginPassword = user.Password
            };

            var response = await this.GetHttpClient().PostAsJsonAsync("/Login/employee", requestBody);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }



        //Helper method to create httpclientfactor CreateClient()
        private HttpClient GetHttpClient()
        {
            return httpClientFactory.CreateClient(apiName);
        }
    }
}
