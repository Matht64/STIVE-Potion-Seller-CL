using System.Text.Json.Serialization;

namespace STIVE.CL.DTOs;

public class AddRoleDto
{
    [JsonPropertyName("name")]
    public string RoleName { get; set; }
}