using System.Text.Json.Serialization;

namespace STIVE.CL.DTOs;

public class CustomerDto
{
    [JsonPropertyName("id")]
    public int CustomerId { get; set; }
    [JsonPropertyName("name")]
    public string CustomerName { get; set; }
    [JsonPropertyName("picture")]
    public string CustomerPicture { get; set; }
}