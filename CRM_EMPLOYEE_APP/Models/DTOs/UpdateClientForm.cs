using System.ComponentModel.DataAnnotations;

namespace CRM_EMPLOYEE_APP.Models.DTOs
{
    public class UpdateClientForm
    {
        [Required]
        public Guid ClientID { get; set; }
        [Required]
        public int TitleId { get; set; }
        [Required]
        public string ClientName { get; set; } = null!;
        [Required]
        public string ClientSurname { get; set; } = null!;
        [Required]
        public string? ClientEmail { get; set; }
        [Required]
        public string ClientContactNumber { get; set; } = null!;
        [Required]
        public string ClientAddress { get; set; } = null!;

        public IFormFile? ClientProfilePicture { get; set; }
        [Required]
        public int TypeId { get; set; }
    }
}
