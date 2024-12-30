using Microsoft.AspNetCore.Mvc;
using Udemy.DataAccess.Data;
using udemy.Models;

namespace udemy.Controllers;

public class  CategoryController : Controller
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
        Console.WriteLine(category);
        if (category.Name == category.DisplayOrder.ToString())
        {
            ModelState.AddModelError("DisplayOrder", "Display order no good");
        }
        
        if (!ModelState.IsValid)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
            TempData["message"] = "Category added";
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
        if (ModelState.IsValid)
        {
            _db.Categories.Update(category);
            _db.SaveChanges();
            TempData["message"] = "Category Edited";
            return RedirectToAction("Index", "Category");
        }
        
        return View();
    }
    
    public IActionResult Delete(int? id)
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

    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        Category? categoryToDelete = _db.Categories.FirstOrDefault(c=> c.Id == id);

        if (categoryToDelete == null)
        {
            return NotFound();
        }
        
        if (ModelState.IsValid)
        {
            _db.Categories.Remove(categoryToDelete);
            _db.SaveChanges();
            return RedirectToAction("Index", "Category");
        }
        
        return View();
    }
}