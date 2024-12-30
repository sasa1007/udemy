using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor.Data;

namespace Razor.Pages.Category;

[BindProperties]
public class CreateModel : PageModel
{
    private readonly AplicationDbContext _db;

    public Model.Category Category { get; set; }

    public CreateModel(AplicationDbContext dbContext)
    {
        _db = dbContext;
    }
    public void OnGet()
    {

    }

    public IActionResult OnPost()
    {
        _db.Categories.Add(Category);
        _db.SaveChanges();
        TempData["message"] = "Category added";
        return RedirectToPage("Index");
    }
}