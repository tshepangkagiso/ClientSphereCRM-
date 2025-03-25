using CRM_API.Data.Services.Interfaces;
using CRM_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CRM_API.Data.Services
{
    public class EmployeeDbServices : IEmployeeDbServices
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeeDbServices(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Query to get all employees with stored procedures
        public async Task<List<Employee>> GetAllEmployees()
        {
            var employees = await this.dbContext.Employees.FromSqlRaw("EXEC spGetAllEmployees").ToListAsync();
            return employees;
        }

        //Query to get employee by id with stored procedure
        public Employee GetEmployeeById(Guid employeeID)
        {
            var employee = this.dbContext.Employees.FromSqlRaw("EXEC spGetEmployeeById @EmployeeID={0}", employeeID).AsEnumerable().FirstOrDefault();
            if(employee == null)  return null;
            return employee;
        }

        //Query to create new employee with stored procedure
        public async Task CreateEmployee(int TitleID, string EmployeeName,string EmployeeSurname,string LoginUsername,string LoginPassword)
        {
            await this.dbContext.Database
                .ExecuteSqlRawAsync("EXEC spCreateEmployee @TitleID={0},@EmployeeName={1},@EmployeeSurname={2},@LoginUsername={3},@LoginPassword={4}",
                TitleID,EmployeeName,EmployeeSurname,LoginUsername,LoginPassword);
        }
    }
}
