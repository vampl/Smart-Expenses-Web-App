using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using smart_expenses_web_app.Data;
using smart_expenses_web_app.Services;

namespace smart_expenses_web_app.Pages.Account;

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

    public IList<Models.Account> Account { get;set; } = default!;

    public async Task OnGetAsync()
    {
        // Get user guid
        var userId = _userService.GetUserId();

        // Get user Accounts
        Account = await _context.Accounts
            .Include(a => a.User)
            .Where(a => a.UserId == userId)
            .ToListAsync();
    }
}