using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using smart_expenses_web_app.Enums;

namespace smart_expenses_web_app.Models;

public class Account
{
    [Key]
    [Display(Name = "Ідентифікатор")]
    public long Id { get; set; }

    [Display(Name = "Ідентифікатор користувача")]
    public string UserId { get; set; } = null!;

    [Display(Name = "Тип рахунку")]
    public AccountType AccountType { get; set; }

    [Display(Name = "Найменування")]
    public string? Title { get; set; }

    [Display(Name = "Баланс")]
    public decimal Amount { get; set; }
    
    [Display(Name = "Валюта")]
    public CurrencyCode CurrencyCode { get; set; }

    [ForeignKey(nameof(UserId))] 
    public virtual User? User { get; set; }

    public virtual ICollection<Transaction>? Transactions { get; set; }
}