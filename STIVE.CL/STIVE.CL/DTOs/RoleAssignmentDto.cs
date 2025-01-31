using System.Text.Json.Serialization;

namespace STIVE.CL.DTOs;

public class RoleAssignmentDto
{
    [JsonPropertyName("userId")]
    public string UserId { get; set; }

    [JsonPropertyName("roleName")]
    public string RoleName { get; set; }
}