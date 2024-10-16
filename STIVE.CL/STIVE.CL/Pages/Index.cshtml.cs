using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace STIVE.CL.Pages;

public class IndexModel : PageModel
{
    private readonly HttpClient _httpClient;

    public IndexModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public int UserCount { get; set; }
    
    public async Task OnGetAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("http://localhost:5294/User/count");
        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            UserCount = JsonSerializer.Deserialize<int>(jsonResponse);
        }
        else
        {
            UserCount = 0;
        }
    }
}