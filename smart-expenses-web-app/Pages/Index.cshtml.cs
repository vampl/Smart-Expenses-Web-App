using Microsoft.AspNetCore.Mvc.RazorPages;

namespace smart_expenses_web_app.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    
    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }
    
    public void OnGet()
    {
    }
}