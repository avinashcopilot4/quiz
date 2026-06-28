using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Common.DTO.User
{
    public class UserDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("roles")]
        public List<string> Roles { get; set; } = new();

        [JsonPropertyName("activeRole")]
        public string ActiveRole { get; set; } = "employee";

        [JsonPropertyName("gender")]
        public string? Gender { get; set; }

        [JsonPropertyName("phoneNumber")]
        public string? PhoneNumber { get; set; }
    }
}
