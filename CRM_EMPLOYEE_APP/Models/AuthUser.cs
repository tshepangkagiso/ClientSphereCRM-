using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CRM_EMPLOYEE_APP.Models
{
    public class AuthUser
    {
        [Required]
        [JsonProperty("loginUsername")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [JsonProperty("loginPassword")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }


}
