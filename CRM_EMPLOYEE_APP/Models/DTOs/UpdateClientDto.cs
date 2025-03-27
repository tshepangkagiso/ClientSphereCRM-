namespace CRM_EMPLOYEE_APP.Models.DTOs
{
    public class UpdateClientDTO
    {
        public Guid ClientID { get; set; }
        public int TitleId { get; set; }

        public string ClientName { get; set; } = null!;

        public string ClientSurname { get; set; } = null!;

        public string? ClientEmail { get; set; }

        public string ClientContactNumber { get; set; } = null!;

        public string ClientAddress { get; set; } = null!;

        public byte[] ClientProfilePicture { get; set; } = Array.Empty<byte>();

        public int TypeId { get; set; }
    }
}
