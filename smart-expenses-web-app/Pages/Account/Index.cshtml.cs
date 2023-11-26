using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using smart_expenses_web_app.Services;

namespace smart_expenses_web_app.Pages.Account;

[Authorize]
public class IndexModel : PageModel
{
    private readonly AccountService _accountService;
    private readonly UserService _userService;
    
    public IndexModel(AccountService accountService, UserService userService)
    {
        // Inject required services
        _accountService = accountService;
        _userService = userService;

        Accounts = new List<Models.Account>();
    }

    public IList<Models.Account>? Accounts { get; set; }

    public async Task OnGetAsync()
    {
        // Get user guid
        var userId = _userService.GetUserId();

        // Get user Accounts
        Accounts = await _accountService.GetUserAccountsAsync(userId);
    }
}