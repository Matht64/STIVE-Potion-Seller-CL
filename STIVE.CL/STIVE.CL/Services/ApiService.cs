using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace STIVE.CL.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<T>> GetListAsync<T>(string url)
    {
        var response = await _httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }
        return new List<T>();
    }

    public async Task<bool> DeleteAsync(string? url)
    {
        try
        {
            var response = await _httpClient.DeleteAsync(url);
        
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Suppression réussie : {url}");
                return true;
            }
            else
            {
                Console.WriteLine($"Erreur lors de la suppression, statut : {response.StatusCode}");
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur de connexion à l'API : {ex.Message}");
            return false;
        }
    }
}
