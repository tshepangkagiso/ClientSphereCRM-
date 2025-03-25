using CRM_API.Data;
using CRM_API.Data.Services.Interfaces;
using CRM_API.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentDbServices commentDbServices;

        public CommentController(ICommentDbServices commentDbServices)
        {
            this.commentDbServices = commentDbServices;
        }



        [HttpGet("{SentBy}/{SentTo}")]
        public async Task<IActionResult> OnGet([FromRoute] Guid SentBy, [FromRoute] Guid SentTo)

        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var comments = await this.commentDbServices.GetAllComments(SentBy, SentTo);
                if (comments == null) return StatusCode(400, "No comments found.");
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> OnPost([FromBody] CreateCommentDto commentDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                await this.commentDbServices.CreateComment(commentDto.CommentMessage, commentDto.CommentSentBy, commentDto.CommentSentTo);
                return Ok(commentDto);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
