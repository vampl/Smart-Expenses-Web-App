using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using smart_expenses_web_app.Data;
using smart_expenses_web_app.Enums;
using smart_expenses_web_app.Services;

namespace smart_expenses_web_app.Pages.Account;

[Authorize]
public class CreateModel : PageModel
{
    private readonly SmartExpensesDataContext _context;
    private readonly UserService _userService;

    public CreateModel(SmartExpensesDataContext context, UserService userService)
    {
        // Inject required services
        _context = context;
        _userService = userService;

        Account = new Models.Account();
    }

    public string? UserId { get; set; }
    public SelectList? AccountTypesSelectList { get; set; }
    public SelectList? CurrencyCodesSelectList { get; set; }
    
    [BindProperty]
    public Models.Account Account { get; set; }
    
    public IActionResult OnGet()
    {
        // Access current session user guid
        UserId = _userService.GetUserId();
        
        // Prepare form account types list
        var accountTypesSelectListItems = Enum.GetValues<AccountType>()
            .Select(accountType => new SelectListItem { Text = accountType.ToString(), Value = accountType.ToString() })
            .ToList();
        AccountTypesSelectList = new SelectList(accountTypesSelectListItems, "Value", "Text");
        
        // Prepare form currency codes list
        var currencyCodesSelectListItems = Enum.GetValues<CurrencyCode>()
            .Select(currencyCode => new SelectListItem { Text = currencyCode.ToString(), Value = currencyCode.ToString() })
            .ToList();
        CurrencyCodesSelectList = new SelectList(currencyCodesSelectListItems, "Value", "Text");
        
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
        
        // Push account to database & save
        _context.Accounts.Add(Account);
        await _context.SaveChangesAsync();
        
        return RedirectToPage("./Index");
    }
}