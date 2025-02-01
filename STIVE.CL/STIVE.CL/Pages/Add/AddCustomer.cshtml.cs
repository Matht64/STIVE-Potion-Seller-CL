using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STIVE.CL.DTOs;

namespace STIVE.CL.Pages.Add;

public class AddCustomerModel : PageModel
{
    private readonly HttpClient _httpClient;

    public AddCustomerModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [BindProperty]
    public AddCustomerDto Customer { get; set; }
    
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();
        
        var jsonData = JsonSerializer.Serialize(Customer); 
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("http://localhost:5294/api/Customer/create", content);

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError(string.Empty, "Something went wrong");
            return Page();
        }
        return RedirectToPage("/Admin");
    }
}