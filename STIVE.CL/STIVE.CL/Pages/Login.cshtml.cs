using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace STIVE.CL.Pages;

public class LoginModel : PageModel
{
    private readonly HttpClient _httpClient;

    public LoginModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [BindProperty] 
    public UserLoginDTO UserLoginDto { get; set; } = new UserLoginDTO();

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        var json = JsonSerializer.Serialize(UserLoginDto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("http://localhost:5294/User/login", content);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToPage("Index");
        }
        ModelState.AddModelError(String.Empty, "Identifiant invalide, veuillez rééessayer.");
        return Page();
    }
}

public class UserLoginDTO
{
    [Required(ErrorMessage = "L'adresse mail est requise.")]
    [EmailAddress(ErrorMessage = "L'adresse mail est invalide.")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Le mot de passe est requis.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}