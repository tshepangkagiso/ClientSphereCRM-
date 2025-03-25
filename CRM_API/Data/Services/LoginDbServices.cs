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
        private readonly IEmployeeDbServices employeeDbServices;
        private readonly IClientDbServices clientDbServices;

        public LoginDbServices(ApplicationDbContext dbContext,IEmployeeDbServices employeeDbServices, IClientDbServices clientDbServices)
        {
            this.dbContext = dbContext;
            this.employeeDbServices = employeeDbServices;
            this.clientDbServices = clientDbServices;
        }

        public async Task<Employee> EmployeeLogin(LoginDto loginDto)
        {
            //login user is unique in db.
            var employeeLogin = await this.dbContext.EmployeeLogins
                .FirstOrDefaultAsync(x => x.LoginUsername == loginDto.LoginUsername);
            
            if (employeeLogin == null) return null;
            if (PasswordServices.IsVerifiedPassword(loginDto.LoginPassword, employeeLogin.LoginPassword) == false) return null;

            var employee = this.employeeDbServices.GetEmployeeById(employeeLogin.EmployeeID);
            if (employee == null) return null;
            return employee;

        }

        public async Task<Client> ClientLogin(LoginDto loginDto)
        {
            var clientLogin = await this.dbContext.ClientsLogins.FirstOrDefaultAsync(x => x.LoginUsername == loginDto.LoginUsername);
            if(clientLogin == null) return null;
            if (PasswordServices.IsVerifiedPassword(loginDto.LoginPassword, clientLogin.LoginPassword) == false) return null;

            var client = this.clientDbServices.GetClientByIdFromStoredProcedure(clientLogin.ClientID);
            if (client == null) return null;
            return client;
        }
    }
}
