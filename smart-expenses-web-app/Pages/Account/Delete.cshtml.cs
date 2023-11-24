using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using smart_expenses_web_app.Data;

namespace smart_expenses_web_app.Pages.Account;

[Authorize]
public class DeleteModel : PageModel
{
    private readonly SmartExpensesDataContext _context;

    public DeleteModel(SmartExpensesDataContext context)
    {
        // Inject required services
        _context = context;

        Account = new Models.Account();
    }

    [BindProperty]
    public Models.Account Account { get; set; }
    
    public async Task<IActionResult> OnGetAsync(long? id)
    {
        // Check if Id passed
        if (id == null)
        {
            return NotFound();
        }

        // Check if account is exist in database
        var account = await _context.Accounts.FirstOrDefaultAsync(account => account.Id == id);
        if (account == null)
        {
            return NotFound();
        }

        // Fill account form
        Account = account;
        
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(long? id)
    {
        // Check if Id passed
        if (id == null)
        {
            return NotFound();
        }

        // Check if account is exist in database
        var account = await _context.Accounts.FindAsync(id);
        if (account == null)
        {
            return RedirectToPage("./Index");
        }
        
        // Delete account from database & save
        Account = account;
        _context.Accounts.Remove(Account);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}