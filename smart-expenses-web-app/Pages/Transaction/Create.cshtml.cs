using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using smart_expenses_web_app.Data;
using smart_expenses_web_app.Enums;
using smart_expenses_web_app.Services;

namespace smart_expenses_web_app.Pages.Transaction;

public class CreateModel : PageModel
{
    private readonly SmartExpensesDataContext _context;
    private readonly UserService _userService;

    public CreateModel(SmartExpensesDataContext context, UserService userService)
    {
        _context = context;
        _userService = userService;
        
        Transaction = new Models.Transaction();
    }

    public string? UserId { get; set; }
    public SelectList? TransactionTypesSelectList { get; set; }
    public SelectList? CurrencyCodesSelectList { get; set; }
    public SelectList? AccountsSelectList { get; set; }

    [BindProperty]
    public Models.Transaction Transaction { get; set; }
    
    public IActionResult OnGet()
    {
        // Access current session user guid
        UserId = _userService.GetUserId();
        
        // Prepare form account types list
        var accountTypesSelectListItems = Enum.GetValues<AccountType>()
            .Select(accountType => new SelectListItem { Text = accountType.ToString(), Value = accountType.ToString() })
            .ToList();
        TransactionTypesSelectList = new SelectList(accountTypesSelectListItems, "Value", "Text");
        
        // Prepare form currency codes list
        var currencyCodesSelectListItems = Enum.GetValues<CurrencyCode>()
            .Select(currencyCode => new SelectListItem { Text = currencyCode.ToString(), Value = currencyCode.ToString() })
            .ToList();
        CurrencyCodesSelectList = new SelectList(currencyCodesSelectListItems, "Value", "Text");
        
        // Prepare form currency codes list
        var accountSelectListItems = _context.Accounts.Where(account => account.UserId == UserId)
            .Select(account => new SelectListItem { Text = account.Title, Value = account.Id.ToString() })
            .ToList();
        AccountsSelectList = new SelectList(accountSelectListItems, "Value", "Text");
        
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

        // Push transactions to database & save
        _context.Transactions.Add(Transaction);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}