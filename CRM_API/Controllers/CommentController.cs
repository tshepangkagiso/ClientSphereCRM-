using CRM_API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public CommentController(ApplicationDbContext dbContext)
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
        [HttpPut]
        public async Task<IActionResult> OnPutAsync()
        {

        }
        [HttpDelete]
        public async Task<IActionResult> OnDeleteAsync()
        {

        }
    }
}
