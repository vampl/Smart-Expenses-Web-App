using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using smart_expenses_web_app.Data;
using smart_expenses_web_app.Enums;
using smart_expenses_web_app.Models;

namespace smart_expenses_web_app.Pages.Account;

[Authorize]
public class CreateModel : PageModel
{
    private readonly SmartExpensesDataContext _context;
    private readonly UserManager<User> _userManager;

    public CreateModel(SmartExpensesDataContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public string? UserId { get; set; }
    
    public List<SelectListItem> AccountTypesList { get; set; } = default!;
    
    public List<SelectListItem> CurrencyCodesList { get; set; } = default!;
    
    public IActionResult OnGet()
    {
        UserId = _userManager.GetUserId(User);

        AccountTypesList = new List<SelectListItem>
        {
            new(AccountType.Cash.ToString(), AccountType.Cash.ToString()),
            new(AccountType.Card.ToString(), AccountType.Card.ToString())
        };
        
        CurrencyCodesList = new List<SelectListItem>
        {
            new(CurrencyCode.EUR.ToString(), (CurrencyCode.EUR).ToString()),
            new(CurrencyCode.UAH.ToString(), (CurrencyCode.UAH).ToString()),
            new(CurrencyCode.USD.ToString(), (CurrencyCode.USD).ToString()),
        };

        return Page();
    }

    [BindProperty]
    public Models.Account Account { get; set; } = default!;
        

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
            
            return Page();
        }

        _context.Accounts.Add(Account);
        
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}