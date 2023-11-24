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
        // Inject required services
        _context = context;

        Transaction = new Models.Transaction();
    }

    [BindProperty] 
    public Models.Transaction Transaction { get; set; }

    public async Task<IActionResult> OnGetAsync(long? id)
    {
        // Check if Id passed
        if (id == null)
        {
            return NotFound();
        }

        // Check if transaction is exist in database
        var transaction = await _context.Transactions.FirstOrDefaultAsync(m => m.Id == id);
        if (transaction == null)
        {
            return NotFound();
        }

        // Fill transaction form
        Transaction = transaction;
        
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(long? id)
    {
        // Check if Id passed
        if (id == null)
        {
            return NotFound();
        }
        
        // Check if transaction is exist in database
        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction == null)
        {
            return RedirectToPage("./Index");
        }
        
        // Delete transaction from database & save
        Transaction = transaction;
        _context.Transactions.Remove(Transaction);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}