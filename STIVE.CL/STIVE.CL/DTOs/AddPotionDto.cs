using System.Text.Json.Serialization;

namespace STIVE.CL.DTOs;

public class AddPotionDto
{
    [JsonPropertyName("name")]
    public string PotionName { get; set; }
    [JsonPropertyName("price")]
    public int PotionPrice { get; set; }
    [JsonPropertyName("picture")]
    public string PotionPicture { get; set; }
}