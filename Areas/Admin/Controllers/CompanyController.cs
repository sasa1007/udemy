using Microsoft.AspNetCore.Mvc;

using udemy.Models;
using udemy.Udemy.DataAccess.Repository;

namespace udemy.Areas.Admin.Controllers;

[Area("Admin")]
public class CompanyController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CompanyController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        List<Company> companies = _unitOfWork.Company.GetAll().ToList();
        return View(companies);
    }

    public IActionResult Upsert(int? id)
    {
        if (id == null || id == 0)
        {
            return View(new Company());
        }
        else
        {
            Company companyObj = _unitOfWork.Company.Get(u=>u.Id==id);
            return View(companyObj);
        }
    }

    [HttpPost]
    public IActionResult Upsert(Company companyObj)
    {
        if (ModelState.IsValid)
        {

            if (companyObj.Id == 0)
            {
                _unitOfWork.Company.Create(companyObj);
            }
            else
            {
                _unitOfWork.Company.Update(companyObj);
            }

            _unitOfWork.Save();
            TempData["message"] = "Company added";
            return RedirectToAction("Index", "Company");
        }
        else
        {
            return View(companyObj);
        }
    }

    public IActionResult Delete(int? id)
    {
        var companyToDelete = _unitOfWork.Company.Get(c => c.Id == id);

        if (companyToDelete == null)
        {
            return Json(new { message = "Product not found." });
        }

        _unitOfWork.Company.Delete(companyToDelete);
        _unitOfWork.Save();
        return Json(new { message = "Product deleted." });
    }
}