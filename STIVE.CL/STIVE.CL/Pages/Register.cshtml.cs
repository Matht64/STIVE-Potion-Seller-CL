using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STIVE.CL.DTOs;

namespace STIVE.CL.Pages;

public class RegisterModel : PageModel
{
    private readonly HttpClient httpClient;

    public RegisterModel(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    [BindProperty]
    public UserRegisterDto UserRegister { get; set; }
    
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        var json = JsonSerializer.Serialize(UserRegister);
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
