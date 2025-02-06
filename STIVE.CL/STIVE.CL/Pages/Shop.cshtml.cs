using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STIVE.CL.DTOs;

namespace STIVE.CL.Pages;

public class ShopModel : PageModel
{
    private readonly HttpClient _httpClient;

    public ShopModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public List<BonusDto> Bonus { get; set; } = new List<BonusDto>();
    public List<GameDataDto> GameDatas { get; set; } = new List<GameDataDto>();
    public string UserId { get; set; }
    [BindProperty]
    public int GameDataId { get; set; }
    [BindProperty]
    public AddCartItemDto AddCartItem { get; set; }
    public async Task<IActionResult> OnGetAsync()
    {
        UserId = "1f43468c-c467-4cdd-a02d-29bd888e09c8";
        
        var bonusResponse = await _httpClient.GetAsync("http://localhost:5294/api/Bonus/getAll");
        if (bonusResponse.IsSuccessStatusCode)
        {
            var bonusJson = await bonusResponse.Content.ReadAsStringAsync();
            Bonus = JsonSerializer.Deserialize<List<BonusDto>>(bonusJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        
        var gameDataResponse = await _httpClient.GetAsync($"http://localhost:5294/api/GameData/getAllByUserId/{UserId}");
        if (gameDataResponse.IsSuccessStatusCode)
        {
            var gameDataJson = await gameDataResponse.Content.ReadAsStringAsync();
            GameDatas = JsonSerializer.Deserialize<List<GameDataDto>>(gameDataJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();
        
        var jsonData = JsonSerializer.Serialize(AddCartItem);
        var content  = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("http://localhost:5294/api/Cart/add", content);
        
        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError(string.Empty, "Cart update failed");
            return Page();
        }

        return RedirectToPage();
    }
}