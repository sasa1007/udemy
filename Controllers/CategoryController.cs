using Microsoft.AspNetCore.Mvc;
using Udemy.DataAccess.Data;
using udemy.Models;
using udemy.Udemy.DataAccess.Repository;

namespace udemy.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryController(ICategoryRepository dbContext)
    {
        _categoryRepository = dbContext;
    }

    public IActionResult Index()
    {
        List<Category> categories = _categoryRepository.GetAll().ToList();
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
            _categoryRepository.Create(category);
            _categoryRepository.Save();
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

        Category? categoryToUpdate = _categoryRepository.Get(c => c.Id == id);

        return View(categoryToUpdate);
    }

    [HttpPost]
    public IActionResult Edit(Category category)
    {
        if (ModelState.IsValid)
        {
            _categoryRepository.Update(category);
            _categoryRepository.Save();
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

        Category? categoryToDelete = _categoryRepository.Get(c => c.Id == id);


        return View(categoryToDelete);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        Category? categoryToDelete = _categoryRepository.Get(c => c.Id == id);

        if (ModelState.IsValid)
        {
            _categoryRepository.Delete(categoryToDelete);
            _categoryRepository.Save();
            return RedirectToAction("Index", "Category");
        }

        return View();
    }
}