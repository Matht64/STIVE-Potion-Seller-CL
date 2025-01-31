using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace STIVE.CL.Pages;

public class PotionUpdateModel : PageModel
{
    private readonly HttpClient _httpClient;

    public PotionUpdateModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }
    [BindProperty]
    public PotionDto Potion { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        if (Id <= 0)
            return NotFound();
        
        var response = await _httpClient.GetAsync($"http://localhost:5294/api/Potion/getById/{Id}");
        
        if (!response.IsSuccessStatusCode)
            return NotFound();
        
        var jsonResponse = await response.Content.ReadAsStringAsync();
        Potion = JsonSerializer.Deserialize<PotionDto>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();
        
        var jsonData = JsonSerializer.Serialize(Potion);
        var content  = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync($"http://localhost:5294/api/Potion/update/{Potion.PotionId}", content);

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError(string.Empty, "Potion update failed");
            return Page();
        }
        
        return RedirectToPage("/Admin");
    }
}

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