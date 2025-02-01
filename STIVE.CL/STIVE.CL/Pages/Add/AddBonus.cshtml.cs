using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STIVE.CL.DTOs;

namespace STIVE.CL.Pages.Add;

public class AddBonusModel : PageModel
{
    private readonly HttpClient _httpClient;

    public AddBonusModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [BindProperty]
    public AddBonusDto Bonus { get; set; }
    
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();
        
        var jsonData = JsonSerializer.Serialize(Bonus); 
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("http://localhost:5294/api/Bonus/create", content);

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError(string.Empty, "Something went wrong");
            return Page();
        }
        return RedirectToPage("/Admin");
    }
}