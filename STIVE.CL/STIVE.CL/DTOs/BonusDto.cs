using System.Text.Json.Serialization;

namespace STIVE.CL.DTOs;

public class BonusDto
{
    [JsonPropertyName("id")]
    public int BonusId { get; set; }
    [JsonPropertyName("name")]
    public string BonusName { get; set; }
    [JsonPropertyName("duration")]
    public int BonusDuration { get; set; }
    [JsonPropertyName("price")]
    public float BonusPrice { get; set; }
}