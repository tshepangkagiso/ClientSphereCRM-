namespace CRM_API.Models.DTOs
{
    public class CreateClientDto
    {
        public int TitleId { get; set; }

        public string ClientName { get; set; } = null!;

        public string ClientSurname { get; set; } = null!;

        public string? ClientEmail { get; set; }

        public string ClientContactNumber { get; set; } = null!;

        public string ClientAddress { get; set; } = null!;

        public string? ClientProfilePicture { get; set; }

        public int TypeId { get; set; }

        public string LoginUsername { get; set; } = null!;

        public string LoginPassword { get; set; } = null!;
    }
}
