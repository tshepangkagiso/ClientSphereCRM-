using CRM_API.Data.Services.Interfaces;
using CRM_API.Models;
using CRM_API.Models.DTOs;
using CRM_API.Services;
using Microsoft.EntityFrameworkCore;

namespace CRM_API.Data.Services
{
    public class LoginDbServices : ILoginDbServices
    {

        private readonly ApplicationDbContext dbContext;

        public LoginDbServices(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<Employee?> EmployeeLogin(LoginDto loginDto)
        {
            var employeeLogin = await dbContext.EmployeeLogins
                .FirstOrDefaultAsync(x => x.LoginUsername == loginDto.LoginUsername);

            if (employeeLogin == null || !PasswordServices.IsVerifiedPassword(loginDto.LoginPassword, employeeLogin.LoginPassword))
                return null;

            return await dbContext.Employees.FirstOrDefaultAsync(x => x.EmployeeID == employeeLogin.EmployeeID);
        }

        public async Task<Client?> ClientLogin(LoginDto loginDto)
        {
            var clientLogin = await dbContext.ClientsLogins
                .FirstOrDefaultAsync(x => x.LoginUsername == loginDto.LoginUsername);

            if (clientLogin == null || !PasswordServices.IsVerifiedPassword(loginDto.LoginPassword, clientLogin.LoginPassword))
                return null;

            return await dbContext.Clients.FirstOrDefaultAsync(x => x.ClientID == clientLogin.ClientID);
        }
    }
}
