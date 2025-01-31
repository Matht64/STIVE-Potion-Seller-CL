using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STIVE.CL.DTOs;
using STIVE.CL.Services;

namespace STIVE.CL.Pages;

public class AdminModel : PageModel
{
    private readonly ApiService _apiService;
    public string TableToRender { get; set; }
    public List<UserDto> Users { get; set; }
    public List<RoleDto> Roles { get; set; }
    public List<GameDataDto> GameDatas { get; set; }
    public List<BonusDto> Bonuses { get; set; }
    public List<PotionDto> Potions { get; set; }
    public List<SupplierDto> Suppliers { get; set; }
    public List<CustomerDto> Customers { get; set; }

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
                Users = await _apiService.GetListAsync<UserDto>($"{baseUrl}User/getAll");
                break;
            case "roles":
                Roles = await _apiService.GetListAsync<RoleDto>($"{baseUrl}Role/getAll");
                break;
            case "gamedatas":
                GameDatas = await _apiService.GetListAsync<GameDataDto>($"{baseUrl}GameData/getAll");
                break;
            case "bonus":
                Bonuses = await _apiService.GetListAsync<BonusDto>($"{baseUrl}Bonus/getAll");
                break;
            case "potions":
                Potions = await _apiService.GetListAsync<PotionDto>($"{baseUrl}Potion/getAll");
                break;
            case "suppliers":
                Suppliers = await _apiService.GetListAsync<SupplierDto>($"{baseUrl}Supplier/getAll");
                break;
            case "customers":
                Customers = await _apiService.GetListAsync<CustomerDto>($"{baseUrl}Customer/getAll");
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
            "potion" => $"{baseUrl}Potion/delete/{id}",
            "supplier" => $"{baseUrl}Supplier/delete/{id}",
            "customers" => $"{baseUrl}Customer/delete/{id}",
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
