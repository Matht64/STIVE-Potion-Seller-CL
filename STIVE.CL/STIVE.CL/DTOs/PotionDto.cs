using System.Text.Json.Serialization;

namespace STIVE.CL.DTOs;

public class PotionDto
{
    [JsonPropertyName("id")]
    public int PotionId { get; set; }
    [JsonPropertyName("name")]
    public string PotionName { get; set; }
    [JsonPropertyName("price")]
    public int PotionPrice { get; set; }
    [JsonPropertyName("picture")]
    public string PotionPicture { get; set; }
}