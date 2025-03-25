using CRM_API.Models;

namespace CRM_API.Data.Services.Interfaces
{
    public interface IEmployeeDbServices
    {
        Task CreateEmployee(int TitleID, string EmployeeName, string EmployeeSurname, string LoginUsername, string LoginPassword);
        Task<List<Employee>> GetAllEmployees();
        Employee GetEmployeeById(Guid employeeID);
    }
}