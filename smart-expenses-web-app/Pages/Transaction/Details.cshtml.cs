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
        _context = context;
    }

    public Models.Transaction Transaction { get; set; } = default!; 

    public async Task<IActionResult> OnGetAsync(long? id)
    {
        if (id == null || _context.Transactions == null)
        {
            return NotFound();
        }

        var transaction = await _context.Transactions.FirstOrDefaultAsync(m => m.Id == id);
        if (transaction == null)
        {
            return NotFound();
        }

        Transaction = transaction;
        return Page();
    }
}