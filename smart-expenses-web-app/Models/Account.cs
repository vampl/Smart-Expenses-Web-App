using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using smart_expenses_web_app.Enums;

namespace smart_expenses_web_app.Models;

public class Account
{
    [Key]
    public long Id { get; set; }

    public string UserId { get; set; } = null!;

    public AccountType AccountType { get; set; }

    public string? Title { get; set; }

    public decimal Amount { get; set; }
    
    public CurrencyCode CurrencyCode { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;
    
    public ICollection<Transaction> Transactions { get; set; } = null!;
}