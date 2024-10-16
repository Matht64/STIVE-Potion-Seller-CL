using Microsoft.AspNetCore.Mvc.RazorPages;

namespace STIVE.CL.Pages;

public class JeuModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public JeuModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }
    public void OnGet()
    {
        
    }
}