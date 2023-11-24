using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using smart_expenses_web_app.Data;

namespace smart_expenses_web_app.Pages.Transaction;

public class DetailsModel : PageModel
{
    private readonly SmartExpensesDataContext _context;

    public DetailsModel(SmartExpensesDataContext context)
    {
        // Inject required services
        _context = context;

        Transaction = new Models.Transaction();
    }

    public Models.Transaction Transaction { get; set; }

    public async Task<IActionResult> OnGetAsync(long? id)
    {
        // Check if Id passed
        if (id == null)
        {
            return NotFound();
        }

        // Check if transaction is exist in database
        var transaction = await _context.Transactions.Include(t => t.Account)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (transaction == null)
        {
            return NotFound();
        }

        // Fill transaction form
        Transaction = transaction;
        
        return Page();
    }
}