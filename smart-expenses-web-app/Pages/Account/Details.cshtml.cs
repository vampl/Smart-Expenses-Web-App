using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using smart_expenses_web_app.Data;

namespace smart_expenses_web_app.Pages.Account;

[Authorize]
public class DetailsModel : PageModel
{
    private readonly SmartExpensesDataContext _context;

    public DetailsModel(SmartExpensesDataContext context)
    {
        _context = context;
        
        Account = new Models.Account();
    }

    public Models.Account Account { get; set; }

    public async Task<IActionResult> OnGetAsync(long? id)
    {
        // Check if Id passed
        if (id == null)
        {
            return NotFound();
        }

        // Check if account is exist in database
        var account = await _context.Accounts.FirstOrDefaultAsync(m => m.Id == id);
        if (account == null)
        {
            return NotFound();
        }

        // Fill account form
        Account = account;
        
        return Page();
    }
}