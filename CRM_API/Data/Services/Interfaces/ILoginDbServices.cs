using CRM_API.Models;
using CRM_API.Models.DTOs;

namespace CRM_API.Data.Services.Interfaces
{
    public interface ILoginDbServices
    {
        Task<Client> ClientLogin(LoginDto loginDto);
        Task<Employee> EmployeeLogin(LoginDto loginDto);
    }
}