
namespace CRM_EMPLOYEE_APP.Models;

public class Comment
{
    public int CommentId { get; set; }

    public string CommentMessage { get; set; } = null!;

    public DateTime? CommentDateTime { get; set; }

    public Guid CommentSentBy { get; set; }

    public Guid? CommentSentTo { get; set; }

    public virtual Client CommentSentByNavigation { get; set; } = null!;

    public virtual Employee? CommentSentTo1 { get; set; }

    public virtual Client? CommentSentToNavigation { get; set; }
}
