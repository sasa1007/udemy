using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using udemy.Models;
using udemy.Udemy.DataAccess.Repository;

namespace udemy.Areas.Customer.Controllers;

[Area("Customer")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    //when you gou to /home/index, home
    public IActionResult Index()
    {
        IEnumerable<Product> products = _unitOfWork.Product.GetAll(include: "Category");
        return View(products);
    }

    public IActionResult Details(int id)
    {
        Product product = _unitOfWork.Product.Get(u=>u.Id == id, include: "Category");
        return View(product);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}