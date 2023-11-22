using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using smart_expenses_web_app.Data;
using smart_expenses_web_app.Enums;
using smart_expenses_web_app.Models;

namespace smart_expenses_web_app.Pages.Transaction;

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

    public List<SelectListItem> TransactionTypesList { get; set; } = new();

    public List<SelectListItem> CurrencyCodesList { get; set; } = new();

    public List<SelectListItem> AccountsIdsList { get; set; } = new();

    [BindProperty]
    public Models.Transaction Transaction { get; set; } = default!;
    
    public IActionResult OnGet()
    {
        UserId = _userManager.GetUserId(User);

        TransactionTypesList = new List<SelectListItem>
        {
            new(TransactionType.Withdraw.ToString(), TransactionType.Withdraw.ToString()),
            new(TransactionType.Deposit.ToString(), TransactionType.Deposit.ToString())
        };
        
        CurrencyCodesList = new List<SelectListItem>
        {
            new(CurrencyCode.EUR.ToString(), (CurrencyCode.EUR).ToString()),
            new(CurrencyCode.UAH.ToString(), (CurrencyCode.UAH).ToString()),
            new(CurrencyCode.USD.ToString(), (CurrencyCode.USD).ToString()),
        };
        
        var accountIds = _context.Accounts
            .Where(a => a.UserId == UserId)
            .Select(a => new { a.Title, a.Id });

        foreach (var item in accountIds)
        {
            var selectListItem = new SelectListItem(item.Title, item.Id.ToString());
            AccountsIdsList.Add(selectListItem);
        }
        
        return Page();
    }

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

        _context.Transactions.Add(Transaction);
        
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}