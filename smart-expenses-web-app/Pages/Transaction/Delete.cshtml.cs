using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using smart_expenses_web_app.Data;
using smart_expenses_web_app.Services;

namespace smart_expenses_web_app.Pages.Transaction;

public class DeleteModel : PageModel
{
    private readonly TransactionService _transactionService;

    public DeleteModel(TransactionService transactionService)
    {
        // Inject required services
        _transactionService = transactionService;

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
        var transaction = await _transactionService.GetTransactionAsync(id);
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
        var transaction = await _transactionService.GetTransactionAsync(id);
        if (transaction == null)
        {
            return RedirectToPage("./Index");
        }
        
        // Delete transaction from database & save
        Transaction = transaction;
        await _transactionService.DeleteUserTransactionAsync(Transaction);

        return RedirectToPage("./Index");
    }
}