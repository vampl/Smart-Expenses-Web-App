using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using smart_expenses_web_app.Data;
using smart_expenses_web_app.Enums;
using smart_expenses_web_app.Services;

namespace smart_expenses_web_app.Pages.Transaction;

public class EditModel : PageModel
{
    private readonly SmartExpensesDataContext _context;
    private readonly UserService _userService;

    public EditModel(SmartExpensesDataContext context, UserService userService)
    {
        // Inject required services
        _context = context;
        _userService = userService;

        Transaction = new Models.Transaction();
    }

    public long? Id { get; set; }
    public string? UserId { get; set; }
    public SelectList? TransactionTypesSelectList { get; set; }
    public SelectList? CurrencyCodesSelectList { get; set; }
    public SelectList? AccountsSelectList { get; set; }
    
    [BindProperty]
    public Models.Transaction Transaction { get; set; }

    public async Task<IActionResult> OnGetAsync(long? id)
    {
        // Check if Id passed
        if (id == null)
        {
            return NotFound();
        }

        // Access current account Id
        Id = id;
        
        // Access current session user guid
        UserId = _userService.GetUserId();
        
        // Prepare form account types list
        var transactionTypesSelectListItems = Enum.GetValues<TransactionType>()
            .Select(transactionType => new SelectListItem { Text = transactionType.ToString(), Value = transactionType.ToString() })
            .ToList();
        TransactionTypesSelectList = new SelectList(transactionTypesSelectListItems, "Value", "Text");
        
        // Prepare form currency codes list
        var currencyCodesSelectListItems = Enum.GetValues<CurrencyCode>()
            .Select(currencyCode => new SelectListItem { Text = currencyCode.ToString(), Value = currencyCode.ToString() })
            .ToList();
        CurrencyCodesSelectList = new SelectList(currencyCodesSelectListItems, "Value", "Text");
        
        // Prepare form accounts list
        var accountsSelectListItems = _context.Accounts.Where(account => account.UserId == UserId)
            .Select(account => new SelectListItem { Text = account.Title, Value = account.Id.ToString() })
            .ToList();
        AccountsSelectList = new SelectList(accountsSelectListItems, "Value", "Text");
        
        // Check if account exist in database
        var transaction =  await _context.Transactions.FirstOrDefaultAsync(transaction => transaction.Id == id);
        if (transaction == null)
        {
            return NotFound();
        }
        
        // Fill account form
        Transaction = transaction;

        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        // Check if can be pushed to database
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
            
            return Page();
        }

        // Set entity state to modified
        _context.Attach(Transaction).State = EntityState.Modified;
        
        try
        {
            // Save changes to database
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            // Check if account exist while error
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