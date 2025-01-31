using System.Text.Json.Serialization;

namespace STIVE.CL.DTOs;

public class UserDto
{
    [JsonPropertyName("id")]
    public string UserId { get; set; }
    [JsonPropertyName("userName")]
    public string UserName { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName("phoneNumber")]
    public string PhoneNumber { get; set; }
    [JsonPropertyName("roles")]
    public List<string> Roles { get; set; }
}