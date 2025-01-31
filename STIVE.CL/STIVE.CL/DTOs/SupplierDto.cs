using System.Text.Json.Serialization;

namespace STIVE.CL.DTOs;

public class SupplierDto
{
    [JsonPropertyName("id")]
    public int SupplierId { get; set; }
    [JsonPropertyName("name")]
    public string SupplierName { get; set; }
    [JsonPropertyName("picture")]
    public string SupplierPicture { get; set; }
    [JsonPropertyName("potionId")]
    public int PotionId { get; set; }
}