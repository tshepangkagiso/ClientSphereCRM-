using System.ComponentModel.DataAnnotations;

namespace CRM_EMPLOYEE_APP.Models.DTOs
{
    public class AuthUserDTO
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Username")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
