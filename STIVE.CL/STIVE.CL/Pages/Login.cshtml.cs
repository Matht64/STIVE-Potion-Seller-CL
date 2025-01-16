using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
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
    public UserLoginDto userLoginDto { get; set; }
    public string ErrorMessage { get; set; }
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();
        
        var loginData = new { userLoginDto.UserName, userLoginDto.Password };

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("http://localhost:5294/api/User/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = JsonSerializer.Deserialize<ResponseModel>(await response.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (responseData != null && !string.IsNullOrEmpty(responseData.Token))
                {
                    Response.Cookies.Append("jwt", responseData.Token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        Expires = DateTime.UtcNow.AddHours(1)
                    });
                    return Redirect("/Index");
                }
            }
            else
            {
                ErrorMessage = "Invalid username or password.";
            }
        }

        return Page();
    }
}

public class UserLoginDto
{
    [Required(ErrorMessage = "Le nom est requise.")]
    public string UserName { get; set; }
    [Required(ErrorMessage = "Le mot de passe est requis.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}

public class ResponseModel
{
    public string UserName { get; set; }
    public string[] Roles { get; set; }
    public string Token { get; set; }
}