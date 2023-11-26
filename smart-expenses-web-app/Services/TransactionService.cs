using Microsoft.EntityFrameworkCore;
using smart_expenses_web_app.Data;
using smart_expenses_web_app.Models;

namespace smart_expenses_web_app.Services;

public class TransactionService
{
    private readonly SmartExpensesDataContext _dataContext;
    
    public TransactionService(SmartExpensesDataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public async Task<IList<Transaction>?> GetUserTransactionsAsync(string? userId)
    {
        return await _dataContext.Transactions
            .Include(transaction => transaction.Account)
            .Where(transaction => transaction.UserId == userId)
            .OrderByDescending(transaction => transaction.DateTime)
            .ToListAsync();
    }
    
    public async Task<Transaction?> GetTransactionAsync(long? id)
    {
        return await _dataContext.Transactions.Include(transaction => transaction.Account)
            .FirstOrDefaultAsync(transaction => transaction.Id == id);
    }

    public async Task AddUserTransactionAsync(Transaction newTransaction)
    {
        await _dataContext.Transactions.AddAsync(newTransaction);
        await _dataContext.SaveChangesAsync();
    }

    public async Task EditUserTransactionAsync(Transaction editTransaction)
    {
        _dataContext.Attach(editTransaction).State = EntityState.Modified;
        
        try
        {
            await _dataContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            // Check if account exist while error
            if (!IsExist(editTransaction.Id))
            {
                return;
            }

            throw;
        }
    }
    
    public async Task DeleteUserTransactionAsync(Transaction deleteTransaction)
    {
        _dataContext.Transactions.Remove(deleteTransaction);
        await _dataContext.SaveChangesAsync();
    }

    public bool IsExist(long id)
    {
        return _dataContext.Transactions.Any(transaction => transaction.Id == id);
    }
}