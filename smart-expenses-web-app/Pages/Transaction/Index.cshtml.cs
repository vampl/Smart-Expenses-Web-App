using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using smart_expenses_web_app.Data;

namespace smart_expenses_web_app.Pages.Transaction;

public class IndexModel : PageModel
{
    private readonly SmartExpensesDataContext _context;

    public IndexModel(SmartExpensesDataContext context)
    {
        _context = context;
    }

    public IList<Models.Transaction> Transaction { get;set; } = default!;

    public async Task OnGetAsync()
    {
        if (_context.Transactions != null)
        {
            Transaction = await _context.Transactions
                .Include(t => t.Account)
                .Include(t => t.User).ToListAsync();
        }
    }
}