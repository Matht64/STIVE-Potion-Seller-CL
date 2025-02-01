using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STIVE.CL.DTOs;

namespace STIVE.CL.Pages.Add;

public class AddSupplierModel : PageModel
{
    private readonly HttpClient _httpClient;

    public AddSupplierModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [BindProperty]
    public AddSupplierDto Supplier { get; set; }
    public List<PotionDto> Potions { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        var potionsResponse = await _httpClient.GetAsync("http://localhost:5294/api/Potion/getAll");
        if (potionsResponse.IsSuccessStatusCode)
        {
            var potionsJson = await potionsResponse.Content.ReadAsStringAsync();
            Potions = JsonSerializer.Deserialize<List<PotionDto>>(potionsJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        
        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();
        
        var jsonData = JsonSerializer.Serialize(Supplier); 
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("http://localhost:5294/api/Supplier/create", content);

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError(string.Empty, "Something went wrong");
            return Page();
        }
        return RedirectToPage("/Admin");
    }
}