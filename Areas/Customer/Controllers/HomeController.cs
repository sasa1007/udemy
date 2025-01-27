using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
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
        ShopingCart cart = new()
        {
            Product = _unitOfWork.Product.Get(u => u.Id == id, include: "Category"),
            Count = 1,
            ProductId = id
        };
        return View(cart);
    }

    [HttpPost]
    [Authorize]
    public IActionResult Details(ShopingCart shopingCart)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
        shopingCart.ApplicationUserId = userId;
        shopingCart.Id = 0;
        ShopingCart shopingCartFromDb =
            _unitOfWork.ShopingCart.Get(u => u.ApplicationUserId == userId && u.ProductId == shopingCart.ProductId);

        if (shopingCartFromDb != null)
        {
            shopingCartFromDb.Count += shopingCartFromDb.Count;
            _unitOfWork.ShopingCart.Update(shopingCartFromDb);
        }
        else
        {
            _unitOfWork.ShopingCart.Create(shopingCart);
        }

        _unitOfWork.Save();

        return RedirectToAction("Index");
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