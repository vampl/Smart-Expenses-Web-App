using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using smart_expenses_web_app.Data;

namespace smart_expenses_web_app.Pages.Transaction;

public class DeleteModel : PageModel
{
    private readonly SmartExpensesDataContext _context;

    public DeleteModel(SmartExpensesDataContext context)
    {
        _context = context;
    }

    [BindProperty] 
    public Models.Transaction Transaction { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var transaction = await _context.Transactions
            .Include(t=> t.Account)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (transaction == null)
        {
            return NotFound();
        }

        Transaction = transaction;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var transaction = await _context.Transactions.FindAsync(id);

        if (transaction == null)
        {
            return RedirectToPage("./Index");
        }
        
        Transaction = transaction;
            
        _context.Transactions.Remove(Transaction);
            
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}