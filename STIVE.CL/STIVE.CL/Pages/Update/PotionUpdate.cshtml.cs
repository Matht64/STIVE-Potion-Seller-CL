using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STIVE.CL.DTOs;

namespace STIVE.CL.Pages.Update;

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
