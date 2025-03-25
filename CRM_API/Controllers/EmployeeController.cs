using CRM_API.Data;
using CRM_API.Data.Services.Interfaces;
using CRM_API.Models.DTOs;
using CRM_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CRM_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeDbServices employeeDbServices;

        public EmployeeController(IEmployeeDbServices employeeDbServices)
        {
            this.employeeDbServices = employeeDbServices;
        }

        [HttpGet]
        public async Task<IActionResult> OnGet()
        {
            try
            {
                var employees = await this.employeeDbServices.GetAllEmployees();
                if (employees != null && employees.Any())
                {
                    return StatusCode(200, employees);
                }
                return StatusCode(400, "No employees were found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{ID}")]
        public IActionResult OnGetByIdAsync([FromRoute] Guid ID)
        {
            try
            {
                var employees = this.employeeDbServices.GetEmployeeById(ID);
                if (employees != null)
                {
                    return StatusCode(200, employees);
                }
                return StatusCode(400, "No employee was found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> OnPost([FromBody] CreateEmployeeDto employeeDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var password = PasswordServices.EncryptPassword(employeeDto.LoginPassword);
                await this.employeeDbServices.CreateEmployee(employeeDto.TitleID,employeeDto.EmployeeName,employeeDto.EmployeeSurname,employeeDto.LoginUsername,password);
                return StatusCode(200, employeeDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
