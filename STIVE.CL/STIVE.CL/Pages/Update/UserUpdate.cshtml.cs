using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace STIVE.CL.Pages;

public class UserUpdateModel : PageModel
{
    private readonly HttpClient _httpClient;

    public UserUpdateModel(HttpClient httpClient) 
    { 
        _httpClient = httpClient;
    }
    
    [BindProperty(SupportsGet = true)]
    public string Id { get; set; }
    [BindProperty]
    public UserDto User { get; set; }
    
    public async Task<IActionResult> OnGetAsync()
    {
        if (string.IsNullOrEmpty(Id))
            return NotFound();
            
        var response = await _httpClient.GetAsync($"http://localhost:5294/api/User/getById/{Id}");
        
        if (!response.IsSuccessStatusCode)
            return NotFound();
        
        var jsonResponse = await response.Content.ReadAsStringAsync();
        User = JsonSerializer.Deserialize<UserDto>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        
        return Page();
    }
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();
        
        var jsonData = JsonSerializer.Serialize(User);
        var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync($"http://localhost:5294/api/User/update/{User.UserId}", content);

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError(string.Empty, "Une erreur est survenue lors de la mise à jour");
            return Page();
        }
        
        return RedirectToPage("/Admin");
    }
}
public class UserDto
{
    [JsonPropertyName("id")]
    public string UserId { get; set; }
    [JsonPropertyName("userName")]
    public string UserName { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName("phoneNumber")]
    public string PhoneNumber { get; set; }
}
