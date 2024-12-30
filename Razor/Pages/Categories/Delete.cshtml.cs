using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Razor.Data;

namespace Razor.Pages.Category;

[BindProperties]
public class Delete : PageModel
{
    private readonly AplicationDbContext _db;

    public Model.Category Category { get; set; }

    public Delete(AplicationDbContext dbContext)
    {
        _db = dbContext;
    }

    public void OnGet(int? id)
    {
        if (id != null || id != 0)
        {
            Category = _db.Categories.Find(id);
        }
    }

    public IActionResult OnPost()
    {
        _db.Categories.Remove(Category);
        _db.SaveChanges();
        return RedirectToPage("Index");
    }
}