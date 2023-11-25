using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using smart_expenses_web_app.Data;
using smart_expenses_web_app.Services;

namespace smart_expenses_web_app.Pages.Account;

[Authorize]
public class DeleteModel : PageModel
{
    private readonly AccountService _accountService;

    public DeleteModel(AccountService accountService)
    {
        // Inject required services
        _accountService = accountService;

        Account = new Models.Account();
    }

    [BindProperty]
    public Models.Account Account { get; set; }
    
    public async Task<IActionResult> OnGetAsync(long? id)
    {
        // Check if Id passed
        if (id == null)
        {
            return NotFound();
        }

        // Check if account is exist in database
        var account = await _accountService.GetAccountAsync(id);
        if (account == null)
        {
            return NotFound();
        }

        // Fill account form
        Account = account;
        
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(long? id)
    {
        // Check if Id passed
        if (id == null)
        {
            return NotFound();
        }

        // Check if account is exist in database
        var account = await _accountService.GetAccountAsync(id);
        if (account == null)
        {
            return RedirectToPage("./Index");
        }
        
        // Delete account from database & save
        Account = account;
        await _accountService.DeleteUserAccount(Account);

        return RedirectToPage("./Index");
    }
}