using System.Text.Json.Serialization;

namespace STIVE.CL.DTOs;

public class AddGameDataDto
{
    [JsonPropertyName("userId")]
    public string UserId { get; set; }
}