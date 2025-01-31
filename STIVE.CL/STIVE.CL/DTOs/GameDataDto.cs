using System.Text.Json.Serialization;

namespace STIVE.CL.DTOs;

public class GameDataDto
{
    [JsonPropertyName("id")]
    public int GameDataId { get; set; }
    [JsonPropertyName("gold")]
    public int Gold { get; set; }
    [JsonPropertyName("userName")]
    public string UserName { get; set; }
}