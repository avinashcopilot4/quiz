using System.Text.Json.Serialization;

namespace Common.DTO;

public class UserProfileUpdateDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("gender")]
    public string? Gender { get; set; }

    [JsonPropertyName("phoneNumber")]
    public string? PhoneNumber { get; set; }
}
