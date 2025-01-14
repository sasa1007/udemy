using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using udemy.Models;
using udemy.Models.ViewModels;
using udemy.Udemy.DataAccess.Repository;

namespace udemy.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        List<Product> products = _unitOfWork.Product.GetAll().ToList();

        return View(products);
    }

    public IActionResult Create()
    {
        ProductVm productVm = new()
        {
            CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Name
            }),
            Product = new Product()
        };
        return View(productVm);
    }

    [HttpPost]
    public IActionResult Create(Product product)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.Product.Create(product);
            _unitOfWork.Save();
            TempData["message"] = "Category added";
            return RedirectToAction("Index", "Product");
        }

        return View();
    }

    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Product? productToUpdate = _unitOfWork.Product.Get(p => p.Id == id);

        return View(productToUpdate);
    }

    [HttpPost]
    public IActionResult Edit(Product product)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.Product.Update(product);
            _unitOfWork.Save();
            TempData["message"] = "Product is Edited";
            return RedirectToAction("Index", "Product");
        }

        return View();
    }

    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Product? productToDelete = _unitOfWork.Product.Get(c => c.Id == id);


        return View(productToDelete);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        Product? productToDelete = _unitOfWork.Product.Get(c => c.Id == id);

        if (ModelState.IsValid)
        {
            _unitOfWork.Product.Delete(productToDelete);
            _unitOfWork.Save();
            return RedirectToAction("Index", "Product");
        }

        return View();
    }
}