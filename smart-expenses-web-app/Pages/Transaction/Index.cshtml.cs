using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using smart_expenses_web_app.Data;
using smart_expenses_web_app.Models;

namespace smart_expenses_web_app.Pages.Transaction;

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

    public IList<Models.Transaction> Transaction { get;set; } = default!;

    public async Task OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        var userId = await _userManager.GetUserIdAsync(user ?? throw new InvalidOperationException());
        
        Transaction = await _context.Transactions
            .Include(t => t.Account)
            .Include(t => t.User)
            .Where(t => t.UserId == userId)
            .ToListAsync();
    }
}