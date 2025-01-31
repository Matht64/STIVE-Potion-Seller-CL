using System.Text.Json.Serialization;

namespace STIVE.CL.DTOs;

public class RoleDto
{
    [JsonPropertyName("id")]
    public string RoleId { get; set; }
    [JsonPropertyName("name")]
    public string RoleName { get; set; }
}