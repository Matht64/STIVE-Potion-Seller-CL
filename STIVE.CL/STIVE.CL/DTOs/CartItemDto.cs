using System.Text.Json.Serialization;

namespace STIVE.CL.DTOs;

public class CartItemDto
{
    public string BonusName { get; set; }
    public float BonusPrice { get; set; }
    [JsonPropertyName("bonusId")]
    public int BonusId { get; set; }
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
}