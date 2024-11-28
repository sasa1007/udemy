using Microsoft.AspNetCore.Mvc;
using udemy.Data;
using udemy.Models;

namespace udemy.Controllers;

public class CategoryController : Controller
{
    private readonly AplicationDbContext _db;

    public CategoryController(AplicationDbContext dbContext)
    {
        _db = dbContext;
    }

    public IActionResult Index()
    {
        List <Category> categories = _db.Categories.ToList();
        return View();
    }
}