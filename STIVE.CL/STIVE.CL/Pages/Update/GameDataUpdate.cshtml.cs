using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STIVE.CL.DTOs;

namespace STIVE.CL.Pages.Update;

public class GameDataUpdateModel : PageModel
{
    private readonly HttpClient _httpClient;

    public GameDataUpdateModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }
    [BindProperty]
    public GameDataDto GameData { get; set; }
    public List<UserDto> Users { get; set; } = new List<UserDto>();

    public async Task<IActionResult> OnGetAsync()
    {
        if (Id <= 0)
            return NotFound();
        
        var response = await _httpClient.GetAsync($"http://localhost:5294/api/GameData/getById/{Id}");
        
        if (!response.IsSuccessStatusCode)
            return NotFound();
        
        var jsonResponse = await response.Content.ReadAsStringAsync();
        GameData = JsonSerializer.Deserialize<GameDataDto>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        var userResponse = await _httpClient.GetAsync("http://localhost:5294/api/User/getAll");
        if (userResponse.IsSuccessStatusCode)
        {
            var userJson = await userResponse.Content.ReadAsStringAsync();
            Users = JsonSerializer.Deserialize<List<UserDto>>(userJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();
        
        var jsonData = JsonSerializer.Serialize(GameData);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync($"http://localhost:5294/api/GameData/update/{GameData.GameDataId}", content);

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError(string.Empty, "Game data update failed");
            return Page();
        }
        return RedirectToPage("/Admin");
    }
}