using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STIVE.CL.Services;

namespace STIVE.CL.Pages;

public class AdminModel : PageModel
{
    private readonly ApiService _apiService;
    public string TableToRender { get; set; }
    public List<User> Users { get; set; }
    public List<Role> Roles { get; set; }
    public List<GameData> GameDatas { get; set; }
    public List<Bonus> Bonuses { get; set; }

    private readonly HttpClient _httpClient;

    public AdminModel(ApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task OnGetAsync(string table = "users")
    {
        TableToRender = table;
        string baseUrl = "http://localhost:5294/api/";
        
        switch (TableToRender)
        {
            case "users":
                Users = await _apiService.GetListAsync<User>($"{baseUrl}User/getAll");
                break;
            case "roles":
                Roles = await _apiService.GetListAsync<Role>($"{baseUrl}Role/getAll");
                break;
            case "gamedatas":
                GameDatas = await _apiService.GetListAsync<GameData>($"{baseUrl}GameData/getAll");
                break;
            case "bonus":
                Bonuses = await _apiService.GetListAsync<Bonus>($"{baseUrl}Bonus/getAll");
                break;
        }
    }
    
    public async Task<IActionResult> OnPostDeleteAsync(string id, string type)
    {
        if (string.IsNullOrEmpty(id))
        {
            ModelState.AddModelError("", "ID invalide.");
            return Page();
        }
        
        string baseUrl = "http://localhost:5294/api/";
        
        string? url = type switch
        {
            "user" => $"{baseUrl}User/delete/{id}",
            "role" => $"{baseUrl}Role/delete/{id}",
            "gamedata" => $"{baseUrl}GameData/delete/{id}",
            "bonus" => $"{baseUrl}Bonus/delete/{id}",
            _ => null
        };

        if (url == null)
            return Page();
        
        if (await _apiService.DeleteAsync(url))
        {
            TempData["Message"] = $"{type} supprimé avec succès.";
            return RedirectToPage(new { table = type + "s" });
        }
        else
        {
            TempData["Error"] = $"Erreur lors de la suppression du {type}.";
            return RedirectToPage(new { table = type + "s" });
        }
    }
}
public class User
{
    [JsonPropertyName("id")]
    public string UserId { get; set; }

    [JsonPropertyName("userName")]
    public string UserName { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("phoneNumber")]
    public string PhoneNumber { get; set; }

    [JsonPropertyName("roles")]
    public List<string> Roles { get; set; }
}
public class Role
{
    [JsonPropertyName("id")]
    public string RoleId { get; set; }
    [JsonPropertyName("name")]
    public string RoleName { get; set; }
}
public class GameData
{
    [JsonPropertyName("id")]
    public int GameDataId { get; set; }
    [JsonPropertyName("gold")]
    public int Gold { get; set; }
    [JsonPropertyName("userName")]
    public string UserName { get; set; }
}

public class Bonus
{
    [JsonPropertyName("id")]
    public int BonusId { get; set; }
    [JsonPropertyName("name")]
    public string BonusName { get; set; }
    [JsonPropertyName("duration")]
    public int BonusDuration { get; set; }
    [JsonPropertyName("price")]
    public float BonusPrice { get; set; }
}