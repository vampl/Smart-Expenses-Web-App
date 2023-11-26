using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using smart_expenses_web_app.Services;

namespace smart_expenses_web_app.Pages.Transaction;

[Authorize]
public class IndexModel : PageModel
{
    private readonly TransactionService _transactionService;
    private readonly UserService _userService;
    
    public IndexModel(TransactionService transactionService, UserService userService)
    {
        // Inject required services
        _transactionService = transactionService;
        _userService = userService;

        Transactions = new List<Models.Transaction>();
    }

    public IList<Models.Transaction>? Transactions { get;set; }

    public async Task OnGetAsync()
    {
        // Get user guid
        var userId = _userService.GetUserId();
        
        // Get user Transaction
        Transactions = await _transactionService.GetUserTransactionsAsync(userId);
    }
}