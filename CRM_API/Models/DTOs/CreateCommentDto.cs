namespace CRM_API.Models.DTOs
{
    public class CreateCommentDto
    {
        public string CommentMessage { get; set; } = null!;

        public Guid CommentSentBy { get; set; }

        public Guid CommentSentTo { get; set; }
    }
}
