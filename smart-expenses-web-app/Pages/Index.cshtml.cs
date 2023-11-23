using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using smart_expenses_web_app.Data;
using smart_expenses_web_app.Models;

namespace smart_expenses_web_app.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly SmartExpensesDataContext _context;
    private readonly UserManager<User> _userManager;
    
    public IndexModel(ILogger<IndexModel> logger, SmartExpensesDataContext context, UserManager<User> userManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
    }

    public List<Models.Transaction> Transactions { get; set; } = new();
    public List<Models.Account> Accounts { get; set; } = new();

    public void OnGet()
    {
        var userId = _userManager.GetUserId(User);

        Transactions = _context.Transactions
            .Include(t => t.Account)
            .Where(t => t.UserId == userId)
            .ToList();
        
        Accounts = _context.Accounts
            .Where(a => a.UserId == userId)
            .ToList();
    }
}