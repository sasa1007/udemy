using Microsoft.AspNetCore.Mvc;

namespace udemy.Controllers;

public class CategoryController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}