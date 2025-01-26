using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace STIVE.CL.Pages;

public class AdminModel : PageModel
{
    public string TableToRender { get; set; }
    public List<User> Users { get; set; }
    public List<Role> Roles { get; set; }

    private readonly HttpClient _httpClient;

    public AdminModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task OnGetAsync(string table = "users")
    {
        TableToRender = table;

        if (TableToRender == "users")
        {
            await LoadUsersAsync();
        }
        else if (TableToRender == "roles")
        {
            await LoadRolesAsync();
        }
    }

    private async Task LoadUsersAsync()
    {
        var response = await _httpClient.GetAsync("http://localhost:5294/api/User/getAll");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            Users = JsonSerializer.Deserialize<List<User>>(json);
        }
        else
        {
            Users = new List<User>();
        }
    }

    private async Task LoadRolesAsync()
    {
        var response = await _httpClient.GetAsync("http://localhost:5294/api/Role/getAll");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            Roles = JsonSerializer.Deserialize<List<Role>>(json);
        }
        else
        {
            Roles = new List<Role>();
        }
    }

    public async Task<IActionResult> OnPostDeleteUserAsync(string userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            ModelState.AddModelError("", "ID utilisateur invalide.");
            return Page();
        }

        var response = await _httpClient.DeleteAsync($"http://localhost:5294/api/User/delete/{userId}");

        if (response.IsSuccessStatusCode)
        {
            TempData["Message"] = "Utilisateur supprimé avec succès.";
            return RedirectToPage(new { table = "users" });
        }
        else
        {
            TempData["Error"] = "Erreur lors de la suppression de l'utilisateur.";
            return Page();
        }
    }
    public async Task<IActionResult> OnPostDeleteRoleAsync(string roleId)
    {
        if (string.IsNullOrEmpty(roleId))
        {
            ModelState.AddModelError("", "ID utilisateur invalide.");
            return Page();
        }

        var response = await _httpClient.DeleteAsync($"http://localhost:5294/api/Role/delete/{roleId}");

        if (response.IsSuccessStatusCode)
        {
            TempData["Message"] = "Utilisateur supprimé avec succès.";
            return RedirectToPage(new { table = "users" });
        }
        else
        {
            TempData["Error"] = "Erreur lors de la suppression de l'utilisateur.";
            return Page();
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
