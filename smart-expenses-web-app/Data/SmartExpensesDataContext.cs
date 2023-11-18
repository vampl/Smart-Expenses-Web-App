using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using smart_expenses_web_app.Models;

namespace smart_expenses_web_app.Data;

public class SmartExpensesDataContext : IdentityDbContext<User>
{
    public SmartExpensesDataContext(DbContextOptions<SmartExpensesDataContext> options)
        : base(options)
    {
    }

    public DbSet<Account> Accounts { get; set; } = null!;

    public DbSet<Transaction> Transactions { get; set; } = null!;
}
