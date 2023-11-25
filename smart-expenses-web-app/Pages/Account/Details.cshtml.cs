using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using smart_expenses_web_app.Data;
using smart_expenses_web_app.Services;

namespace smart_expenses_web_app.Pages.Account;

[Authorize]
public class DetailsModel : PageModel
{
    private readonly AccountService _accountService;

    public DetailsModel(AccountService accountService)
    {
        _accountService = accountService;
        
        Account = new Models.Account();
    }

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
}