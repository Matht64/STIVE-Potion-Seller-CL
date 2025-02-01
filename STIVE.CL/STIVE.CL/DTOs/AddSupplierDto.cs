using System.Text.Json.Serialization;

namespace STIVE.CL.DTOs;

public class AddSupplierDto
{
    [JsonPropertyName("name")]
    public string SupplierName { get; set; }
    [JsonPropertyName("picture")]
    public string SupplierPicture { get; set; }
    [JsonPropertyName("potionId")]
    public int PotionId { get; set; }
}