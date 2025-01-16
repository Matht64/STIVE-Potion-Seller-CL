using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace STIVE.CL.Pages;

public class RegisterModel : PageModel
{
    private readonly HttpClient httpClient;

    public RegisterModel(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    [BindProperty]
    public UserRegisterDTO UserRegisterDto { get; set; } = new UserRegisterDTO();
    
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        var json = JsonSerializer.Serialize(UserRegisterDto);
        var content  = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("http://localhost:5294/api/User/register", content);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToPage("/Login");
        }
        ModelState.AddModelError(string.Empty, "Un problème est survenu, veuillez rééessayer.");
        return Page();
    }
}

public class UserRegisterDTO
{
    [Required(ErrorMessage = "Le nom d'utilisateur est requis.")]
    [StringLength(50, ErrorMessage = "Le nom d'utilisateur doit contenir entre 3 et 50 caractères.", MinimumLength = 3)]
    public string UserName { get; set; }

    [Required(ErrorMessage = "L'adresse mail est requise.")]
    [EmailAddress(ErrorMessage = "L'adresse mail n'est pas valide.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Le numéro de téléphone est requis.")]
    [Phone(ErrorMessage = "Le numéro de téléphone n'est pas valide.")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Le mot de passe est requis.")]
    [DataType(DataType.Password)]
    [StringLength(100, ErrorMessage = "Le mot de passe doit contenir au moins {2} caractères.", MinimumLength = 6)]
    public string Password { get; set; }

    [Required(ErrorMessage = "La confirmation du mot de passe est requise.")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Le mot de passe et sa confirmation ne correspondent pas.")]
    public string ConfirmPassword { get; set; }
}