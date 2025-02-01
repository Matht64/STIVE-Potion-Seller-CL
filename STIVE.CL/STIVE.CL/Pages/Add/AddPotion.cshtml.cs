using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STIVE.CL.DTOs;

namespace STIVE.CL.Pages.Add;

public class AddPotionModel : PageModel
{
    private readonly HttpClient _httpClient;

    public AddPotionModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [BindProperty]
    public AddPotionDto Potion { get; set; }
    
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();
        
        var jsonData = JsonSerializer.Serialize(Potion); 
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("http://localhost:5294/api/Potion/create", content);

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError(string.Empty, "Something went wrong");
            return Page();
        }
        return RedirectToPage("/Admin");
    }
}