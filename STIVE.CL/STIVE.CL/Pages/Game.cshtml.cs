using Microsoft.AspNetCore.Mvc.RazorPages;

namespace STIVE.CL.Pages;

public class GameModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public GameModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }
    public void OnGet()
    {
        
    }
}