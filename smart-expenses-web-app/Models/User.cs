using Microsoft.AspNetCore.Identity;

namespace smart_expenses_web_app.Models;

public class User : IdentityUser
{
    public ICollection<Account>? Accounts { get; set; }

    public ICollection<Transaction>? Transactions { get; set; }
}

