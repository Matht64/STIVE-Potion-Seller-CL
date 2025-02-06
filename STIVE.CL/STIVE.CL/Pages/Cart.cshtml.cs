using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STIVE.CL.DTOs;

namespace STIVE.CL.Pages;

public class CartModel : PageModel
{
    private readonly HttpClient _httpClient;

    public CartModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public List<CartDto> UserCarts { get; set; } = new List<CartDto>();
    [BindProperty]
    public string UserId { get; set; }
    [BindProperty]
    public int BonusId { get; set; }
    [BindProperty]
    public int GameDataId { get; set; }


    public async Task<IActionResult> OnGetAsync()
    {
        UserId = "1f43468c-c467-4cdd-a02d-29bd888e09c8";
        
        var cartsResponse = await _httpClient.GetAsync($"http://localhost:5294/api/Cart/getByUserId/{UserId}");
        if (cartsResponse.IsSuccessStatusCode)
        {
            var cartsJson = await cartsResponse.Content.ReadAsStringAsync();
            UserCarts = JsonSerializer.Deserialize<List<CartDto>>(cartsJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        var requestData = new { userId = UserId, bonusId = BonusId, gameDataId = GameDataId };
        var json = JsonSerializer.Serialize(requestData);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("http://localhost:5294/api/Cart/remove", content);

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError(string.Empty, "Erreur lors de la suppression.");
        }

        return RedirectToPage();
    }
}