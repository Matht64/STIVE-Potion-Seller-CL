using System.Text.Json;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace STIVE.CL.Pages;

public class ClassementModel : PageModel
{
    private readonly HttpClient _httpClient;

    public ClassementModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public List<UserGold>? Users { get; set; }
    
    public async Task OnGetAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("http://localhost:5294/api/User/getUserGold");
        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            Users = JsonSerializer.Deserialize<List<UserGold>>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            });
        }
        else
        {
            Users = new List<UserGold>();
        }
    }

    public class UserGold
    {
        public string UserName { get; set; }
        public int Gold { get; set; }
    }
}
