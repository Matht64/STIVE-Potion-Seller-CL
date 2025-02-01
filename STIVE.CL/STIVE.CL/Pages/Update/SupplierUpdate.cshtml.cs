using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STIVE.CL.DTOs;

namespace STIVE.CL.Pages.Update;

public class SupplierUpdateModel : PageModel
{
    private readonly HttpClient _httpClient;

    public SupplierUpdateModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }
    [BindProperty]
    public SupplierDto Supplier { get; set; }
    public List<PotionDto> Potions { get; set; } = new List<PotionDto>();

    public async Task<IActionResult> OnGetAsync()
    {
        if (Id <= 0)
            return NotFound();
        
        var response = await _httpClient.GetAsync($"http://localhost:5294/api/Supplier/getById/{Id}");
        
        if (!response.IsSuccessStatusCode)
            return NotFound();
        
        var jsonResponse = await response.Content.ReadAsStringAsync();
        Supplier = JsonSerializer.Deserialize<SupplierDto>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        
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
        var content  = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync($"http://localhost:5294/api/Supplier/update/{Supplier.SupplierId}", content);

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError(string.Empty, "Supplier update failed");
            return Page();
        }
        
        return RedirectToPage("/Admin");
    }
}