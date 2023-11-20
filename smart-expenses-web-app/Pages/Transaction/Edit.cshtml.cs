using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using smart_expenses_web_app.Data;

namespace smart_expenses_web_app.Pages.Transaction;

public class EditModel : PageModel
{
    private readonly SmartExpensesDataContext _context;

    public EditModel(SmartExpensesDataContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Models.Transaction Transaction { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(long? id)
    {
        if (id == null || _context.Transactions == null)
        {
            return NotFound();
        }

        var transaction =  await _context.Transactions.FirstOrDefaultAsync(m => m.Id == id);
        if (transaction == null)
        {
            return NotFound();
        }
        Transaction = transaction;
        ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Id");
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

        _context.Attach(Transaction).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TransactionExists(Transaction.Id))
            {
                return NotFound();
            }

            throw;
        }

        return RedirectToPage("./Index");
    }

    private bool TransactionExists(long id)
    {
        return (_context.Transactions?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}