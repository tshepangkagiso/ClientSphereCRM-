using CRM_API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> OnGetAsync()
        {

        }
        [HttpGet("id")]
        public async Task<IActionResult> OnGetByIdAsync()
        {

        }
        [HttpPost]
        public async Task<IActionResult> OnPostAsync()
        {

        }
    }
}
