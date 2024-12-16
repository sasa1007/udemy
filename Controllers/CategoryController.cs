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
        List<Category> categories = _db.Categories.ToList();
        return View(categories);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Category category)
    {
        if (category.Name == category.DisplayOrder.ToString())
        {
            ModelState.AddModelError("DisplayOrder", "Display order no good");
        }

        if (!ModelState.IsValid)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
            return RedirectToAction("Index", "Category");
        }

        return View();
    }

    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Category? categoryToUpdate = _db.Categories.FirstOrDefault(c=> c.Id == id);

        if (categoryToUpdate == null)
        {
            return NotFound();
        }

        return View(categoryToUpdate);
    }

    [HttpPost]
    public IActionResult Edit(Category category)
    {
    }
}