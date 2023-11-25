using Microsoft.EntityFrameworkCore;
using smart_expenses_web_app.Data;
using smart_expenses_web_app.Models;

namespace smart_expenses_web_app.Services;

public class AccountService
{
    private readonly SmartExpensesDataContext _dataContext;
    
    public AccountService(SmartExpensesDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<IList<Account>?> GetUserAccountsAsync(string? userId)
    {
        return await _dataContext.Accounts.Where(account => account.UserId == userId).ToListAsync();
    }
    
    public async Task<Account?> GetAccountAsync(long? id)
    {
        return await _dataContext.Accounts.FirstOrDefaultAsync(account => account.Id == id);
    }

    public async Task AddUserAccountAsync(Account account)
    {
        await _dataContext.Accounts.AddAsync(account);
        await _dataContext.SaveChangesAsync();
    }

    public async Task EditUserAccountAsync(Account editAccount)
    {
        _dataContext.Attach(editAccount).State = EntityState.Modified;
        
        try
        {
            await _dataContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            // Check if account exist while error
            if (!IsExist(editAccount.Id))
            {
                return;
            }

            throw;
        }
    }
    
    public async Task DeleteUserAccount(Account deleteAccount)
    {
        _dataContext.Accounts.Remove(deleteAccount);
        await _dataContext.SaveChangesAsync();
    }

    public bool IsExist(long id)
    {
        return _dataContext.Accounts.Any(e => e.Id == id);
    }
}