using Microsoft.AspNetCore.Mvc.RazorPages;

namespace STIVE.CL.Pages;

public class BoutiqueModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public BoutiqueModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }
    public void OnGet()
    {
        
    }
}