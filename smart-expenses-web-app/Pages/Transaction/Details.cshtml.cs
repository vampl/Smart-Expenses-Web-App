using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using smart_expenses_web_app.Services;

namespace smart_expenses_web_app.Pages.Transaction;

public class DetailsModel : PageModel
{
    private readonly TransactionService _transactionService;

    public DetailsModel(TransactionService transactionService)
    {
        // Inject required services
        _transactionService = transactionService;

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
        var transaction = await _transactionService.GetTransactionAsync(id);
        if (transaction == null)
        {
            return NotFound();
        }

        // Fill transaction form
        Transaction = transaction;
        
        return Page();
    }
}