using CRM_API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginEmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public LoginEmployeeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAsync()
        {

        }
    }
}
