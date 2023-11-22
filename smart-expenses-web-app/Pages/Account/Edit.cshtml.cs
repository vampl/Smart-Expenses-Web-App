using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using smart_expenses_web_app.Data;
using smart_expenses_web_app.Enums;
using smart_expenses_web_app.Models;

namespace smart_expenses_web_app.Pages.Account;

[Authorize]
public class EditModel : PageModel
{
    private readonly SmartExpensesDataContext _context;
    private readonly UserManager<User> _userManager;

    public EditModel(SmartExpensesDataContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public long? Id { get; set; }
    
    public string? UserId { get; set; }
    
    public List<SelectListItem> AccountTypesList { get; set; } = default!;
    
    public List<SelectListItem> CurrencyCodesList { get; set; } = default!;
    
    [BindProperty]
    public Models.Account Account { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Id = id;
        
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
        
        var account =  await _context.Accounts.FirstOrDefaultAsync(m => m.Id == id);
        
        if (account == null)
        {
            return NotFound();
        }
        
        Account = account;
        
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(Account).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AccountExists(Account.Id))
            {
                return NotFound();
            }

            throw;
        }

        return RedirectToPage("./Index");
    }

    private bool AccountExists(long id)
    {
        return (_context.Accounts?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}