using Microsoft.AspNetCore.Mvc;
using udemy.Models;
using udemy.Udemy.DataAccess.Repository;

namespace udemy.Areas.Admin.Controllers;





[Area("Admin")]
public class CategoryController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        List<Category> categories = _unitOfWork.Category.GetAll().ToList();
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
            _unitOfWork.Category.Create(category);
            _unitOfWork.Save();
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

        Category? categoryToUpdate = _unitOfWork.Category.Get(c => c.Id == id);

        return View(categoryToUpdate);
    }

    [HttpPost]
    public IActionResult Edit(Category category)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.Category.Update(category);
            _unitOfWork.Save();
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

        Category? categoryToDelete = _unitOfWork.Category.Get(c => c.Id == id);


        return View(categoryToDelete);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        Category? categoryToDelete = _unitOfWork.Category.Get(c => c.Id == id);

        if (ModelState.IsValid)
        {
            _unitOfWork.Category.Delete(categoryToDelete);
            _unitOfWork.Save();
            return RedirectToAction("Index", "Category");
        }

        return View();
    }
}