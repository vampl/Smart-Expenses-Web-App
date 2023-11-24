using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using smart_expenses_web_app.Data;
using smart_expenses_web_app.Services;

namespace smart_expenses_web_app.Pages;

public class IndexModel : PageModel
{
    private readonly SmartExpensesDataContext _context;

    private readonly UserService _userService;
    
    public IndexModel(SmartExpensesDataContext context, UserService userService)
    {
        _context = context;
        _userService = userService;

        Transactions = new List<Models.Transaction>();
        Accounts = new List<Models.Account>();
    }

    public List<Models.Transaction> Transactions { get; set; }
    public List<Models.Account> Accounts { get; set; }

    public void OnGet()
    {
        var userId = _userService.GetUserId();

        Transactions = _context.Transactions
            .Include(t => t.Account)
            .Where(t => t.UserId == userId)
            .ToList();
        
        Accounts = _context.Accounts
            .Where(a => a.UserId == userId)
            .ToList();
    }
}