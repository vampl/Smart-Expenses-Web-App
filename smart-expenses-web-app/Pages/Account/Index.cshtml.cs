using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using smart_expenses_web_app.Data;

namespace smart_expenses_web_app.Pages.Account;

public class IndexModel : PageModel
{
    private readonly SmartExpensesDataContext _context;

    public IndexModel(SmartExpensesDataContext context)
    {
        _context = context;
    }

    public IList<Models.Account> Account { get;set; } = default!;

    public async Task OnGetAsync()
    {
        if (_context.Accounts != null)
        {
            Account = await _context.Accounts
                .Include(a => a.User).ToListAsync();
        }
    }
}