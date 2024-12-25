using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor.Data;


namespace Razor.Pages.Category;

public class Index : PageModel
{
    private readonly AplicationDbContext _db;

    public List<Model.Category> CateforyList { get; set; }

    public Index(AplicationDbContext dbContext)
    {
        _db = dbContext;
    }

    public void OnGet()
    {
        CateforyList = _db.Categories.ToList();
    }
}