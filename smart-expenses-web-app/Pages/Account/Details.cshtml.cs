using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using smart_expenses_web_app.Data;

namespace smart_expenses_web_app.Pages.Account;

public class DetailsModel : PageModel
{
    private readonly SmartExpensesDataContext _context;

    public DetailsModel(SmartExpensesDataContext context)
    {
        _context = context;
    }

    public Models.Account Account { get; set; } = default!; 

    public async Task<IActionResult> OnGetAsync(long? id)
    {
        if (id == null || _context.Accounts == null)
        {
            return NotFound();
        }

        var account = await _context.Accounts.FirstOrDefaultAsync(m => m.Id == id);
        if (account == null)
        {
            return NotFound();
        }

        Account = account;
        return Page();
    }
}