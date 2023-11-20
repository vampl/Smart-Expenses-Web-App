using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using smart_expenses_web_app.Data;

namespace smart_expenses_web_app.Pages.Transaction;

public class CreateModel : PageModel
{
    private readonly SmartExpensesDataContext _context;

    public CreateModel(SmartExpensesDataContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Id");
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
        return Page();
    }

    [BindProperty]
    public Models.Transaction Transaction { get; set; } = default!;
        

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid || _context.Transactions == null || Transaction == null)
        {
            return Page();
        }

        _context.Transactions.Add(Transaction);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}