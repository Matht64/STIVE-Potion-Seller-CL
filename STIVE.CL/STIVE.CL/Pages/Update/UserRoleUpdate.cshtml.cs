using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace STIVE.CL.Pages;

public class UserRoleUpdateModel : PageModel
{
    private readonly HttpClient _httpClient;

    public UserRoleUpdateModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [BindProperty(SupportsGet = true)]
    public string Id { get; set; }

    [BindProperty]
    public List<string> Roles { get; set; }

    public bool HasAdminRole => Roles != null && Roles.Contains("Admin");

    public RoleAssignmentDto RoleAssignmentDto { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        if (string.IsNullOrEmpty(Id))
            return NotFound();

        var response = await _httpClient.GetAsync($"http://localhost:5294/api/Role/getUserRoles/{Id}");

        if (!response.IsSuccessStatusCode)
            return NotFound();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        Roles = JsonSerializer.Deserialize<List<string>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return Page();
    }
    
    public async Task<IActionResult> OnPostAssignAdminAsync()
    {
        return await ModifyRoleAsync("assignRoles");
    }

    public async Task<IActionResult> OnPostRemoveAdminAsync()
    {
        return await ModifyRoleAsync("removeRoles");
    }

    private async Task<IActionResult> ModifyRoleAsync(string action)
    {
        if (string.IsNullOrEmpty(Id))
            return NotFound();

        var roleDto = new RoleAssignmentDto
        {
            UserId = Id,
            RoleName = "Admin"
        };

        var jsonData = JsonSerializer.Serialize(roleDto);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"http://localhost:5294/api/Role/{action}", content);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, $"Erreur lors de la modification du rôle : {errorContent}");
            return Page();
        }

        // Recharger les rôles après modification
        await OnGetAsync();
        return Page();
    }
}

public class RoleAssignmentDto
{
    [JsonPropertyName("userId")]
    public string UserId { get; set; }

    [JsonPropertyName("roleName")]
    public string RoleName { get; set; }
}