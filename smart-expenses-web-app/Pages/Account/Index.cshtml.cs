using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using smart_expenses_web_app.Data;
using smart_expenses_web_app.Models;

namespace smart_expenses_web_app.Pages.Account;

[Authorize]
public class IndexModel : PageModel
{
    private readonly SmartExpensesDataContext _context;
    private readonly UserManager<User> _userManager;
    
    public IndexModel(SmartExpensesDataContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public IList<Models.Account> Account { get;set; } = default!;

    public async Task OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        var userId = await _userManager.GetUserIdAsync(user ?? throw new InvalidOperationException());

        Account = await _context.Accounts
            .Include(a => a.User)
            .Where(a => a.UserId == userId)
            .ToListAsync();
    }
}