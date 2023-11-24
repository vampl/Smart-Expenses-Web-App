using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using smart_expenses_web_app.Data;
using smart_expenses_web_app.Services;

namespace smart_expenses_web_app.Pages.Transaction;

[Authorize]
public class IndexModel : PageModel
{
    private readonly SmartExpensesDataContext _context;
    private readonly UserService _userService;
    
    public IndexModel(SmartExpensesDataContext context, UserService userService)
    {
        // Inject required services
        _context = context;
        _userService = userService;
    }

    public IList<Models.Transaction> Transaction { get;set; } = default!;

    public async Task OnGetAsync()
    {
        // Get user guid
        var userId = _userService.GetUserId();
        
        // Get user Transaction
        Transaction = await _context.Transactions
            .Include(t => t.Account)
            .Include(t => t.User)
            .Where(t => t.UserId == userId)
            .ToListAsync();
    }
}