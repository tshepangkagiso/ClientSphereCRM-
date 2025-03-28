using CRM_EMPLOYEE_APP.Models;

namespace CRM_EMPLOYEE_APP.Http.Interfaces
{
    public interface IEmployeeWebExecutor
    {
        Task<T?> Login<T>(AuthUser user);
    }
}