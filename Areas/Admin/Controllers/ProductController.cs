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
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
        List<Product> products = _unitOfWork.Product.GetAll().ToList();

        return View(products);
    }

    public IActionResult Upsert(int? id)
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
        if (id == null || id == 0)
        {
            return View(productVm);
        }
        else
        {
            productVm.Product = _unitOfWork.Product.Get(u=>u.Id==id);
            return View(productVm);
        }
    }

    [HttpPost]
    public IActionResult Upsert(ProductVm productVm, IFormFile file)
    {
        if (ModelState.IsValid)
        {
            string wwwrootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwrootPath, @"images/product");

                using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                productVm.Product.ImageUrl = @"images\product\" + fileName;
            }
            _unitOfWork.Product.Create(productVm.Product);
            _unitOfWork.Save();
            TempData["message"] = "Category added";
            return RedirectToAction("Index", "Product");
        }
        else
        {
            productVm.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Name
            });
            return View(productVm);
        }
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