using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using smart_expenses_web_app.Data;

namespace smart_expenses_web_app.Pages.Account;

public class CreateModel : PageModel
{
    private readonly SmartExpensesDataContext _context;

    public CreateModel(SmartExpensesDataContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
        return Page();
    }

    [BindProperty]
    public Models.Account Account { get; set; } = default!;
        

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid || _context.Accounts == null || Account == null)
        {
            return Page();
        }

        _context.Accounts.Add(Account);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}