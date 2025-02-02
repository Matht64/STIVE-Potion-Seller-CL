using System.Text.Json.Serialization;

namespace STIVE.CL.DTOs;

public class AddBonusDto
{
    [JsonPropertyName("name")]
    public string BonusName { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
    [JsonPropertyName("duration")]
    public int BonusDuration { get; set; }
    [JsonPropertyName("price")]
    public float BonusPrice { get; set; }
}