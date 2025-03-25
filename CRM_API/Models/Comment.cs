using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM_API.Models;

public partial class Comment
{
    [Key]
    public int CommentId { get; set; }

    public string CommentMessage { get; set; } = null!;

    public DateTime? CommentDateTime { get; set; }

    public Guid CommentSentBy { get; set; }

    public Guid? CommentSentTo { get; set; }

    public virtual Client CommentSentByNavigation { get; set; } = null!;

    public virtual Employee? CommentSentTo1 { get; set; }

    public virtual Client? CommentSentToNavigation { get; set; }
}
