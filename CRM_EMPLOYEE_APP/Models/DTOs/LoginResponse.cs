using Newtonsoft.Json;

namespace CRM_EMPLOYEE_APP.Models.DTOs
{
    public class LoginResponse()
    {
        [JsonProperty("ID")]
        public Guid Id { get; set; }

        [JsonProperty("Token")]
        public string Token { get; set; } = string.Empty;

        [JsonProperty("ExpireAt")]
        public DateTime ExpiresAt { get; set; }
    }
}
