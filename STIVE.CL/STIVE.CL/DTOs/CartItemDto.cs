using System.Text.Json.Serialization;

namespace STIVE.CL.DTOs;

public class CartItemDto
{
    [JsonPropertyName("userId")]
    public string UserId { get; set; }
    [JsonPropertyName("gameDataId")]

    public int GameDataId { get; set; }
    [JsonPropertyName("bonusId")]
    public int BonusId { get; set; }
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
}