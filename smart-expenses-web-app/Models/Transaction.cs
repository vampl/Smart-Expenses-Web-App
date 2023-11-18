using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using smart_expenses_web_app.Enums;

namespace smart_expenses_web_app.Models;

public class Transaction
{
    [Key]
    public long Id { get; set; }

    public string UserId { get; set; } = null!;
    
    public long AccountId { get; set; }

    public TransactionType TransactionType { get; set; }
    
    public string? Title { get; set; }
    
    public decimal Amount { get; set; }

    public CurrencyCode CurrencyCode { get; set; }

    public DateTime DateTime { get; set; }
    
    public string? Description { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;

    [ForeignKey(nameof(AccountId))]
    public Account Account { get; set; } = null!;
}