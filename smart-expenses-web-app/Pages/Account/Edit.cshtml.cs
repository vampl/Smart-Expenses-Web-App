using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using smart_expenses_web_app.Data;

namespace smart_expenses_web_app.Pages.Account;

public class EditModel : PageModel
{
    private readonly SmartExpensesDataContext _context;

    public EditModel(SmartExpensesDataContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Models.Account Account { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(long? id)
    {
        if (id == null || _context.Accounts == null)
        {
            return NotFound();
        }

        var account =  await _context.Accounts.FirstOrDefaultAsync(m => m.Id == id);
        if (account == null)
        {
            return NotFound();
        }
        Account = account;
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(Account).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AccountExists(Account.Id))
            {
                return NotFound();
            }

            throw;
        }

        return RedirectToPage("./Index");
    }

    private bool AccountExists(long id)
    {
        return (_context.Accounts?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}