
namespace CRM_EMPLOYEE_APP.Models
{
    public class Client
    {
        public Guid ClientID { get; set; }

        public int? TitleID { get; set; }

        public string ClientName { get; set; } = null!;

        public string ClientSurname { get; set; } = null!;

        public string? ClientEmail { get; set; }

        public string ClientContactNumber { get; set; } = null!;

        public string ClientAddress { get; set; } = null!;

        public byte[] ClientProfilePicture { get; set; } = Array.Empty<byte>();

        public int? TypeID { get; set; }

        public virtual ICollection<ClientsLogin> ClientsLogins { get; set; } = new List<ClientsLogin>();

        public virtual ICollection<Comment> CommentCommentSentByNavigations { get; set; } = new List<Comment>();

        public virtual ICollection<Comment> CommentCommentSentToNavigations { get; set; } = new List<Comment>();

        public virtual Title? Title { get; set; }

        public virtual ClientType? Type { get; set; }
    }
}
