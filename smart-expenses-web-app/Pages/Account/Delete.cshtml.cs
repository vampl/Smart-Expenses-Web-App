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
        _context = context;
    }

    [BindProperty]
    public Models.Account Account { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(long? id)
    {
        if (id == null)
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

    public async Task<IActionResult> OnPostAsync(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        
        var account = await _context.Accounts.FindAsync(id);

        if (account == null)
        {
            return RedirectToPage("./Index");
        }
        
        Account = account;
        
        _context.Accounts.Remove(Account);
        
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}