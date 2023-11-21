using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using smart_expenses_web_app.Enums;

namespace smart_expenses_web_app.Models;

public class Transaction
{
    [Key]
    [Display(Name = "Ідентифікатор")]
    public long Id { get; set; }

    [Display(Name = "Ідентифікатор користувача")]
    public string UserId { get; set; } = null!;
    
    [Display(Name = "Ідентифікатор рахунку")]
    public long AccountId { get; set; }

    [Display(Name = "Тип транзакцій")]
    public TransactionType TransactionType { get; set; }
    
    [Display(Name = "Найменування")]
    public string? Title { get; set; }
    
    [Display(Name = "Сума")]
    public decimal Amount { get; set; }
    
    [Display(Name = "Валюта")]
    public CurrencyCode CurrencyCode { get; set; }

    [Display(Name = "Дата")]
    [DataType(DataType.Date)]
    public DateTime DateTime { get; set; }
    
    [Display(Name = "Опис")]
    public string? Description { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;

    [ForeignKey(nameof(AccountId))]
    public Account Account { get; set; } = null!;
}