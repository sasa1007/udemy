using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using udemy.Models;
using udemy.Models.ViewModels;
using udemy.Udemy.DataAccess.Repository;
using udemy.Udemy.Utility;

namespace udemy.Areas.Customer.Controllers;

[Area("Customer")]
[Authorize]
public class CartController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    [BindProperty] public ShoppingCartVm ShoppingCartVm { get; set; }

    public CartController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

        ShoppingCartVm = new()
        {
            ShopingCartList = _unitOfWork.ShopingCart.GetAll(u => u.ApplicationUserId == userId, include: "Product"),
            OrderHeader = new()
        };

        foreach (var cartItem in ShoppingCartVm.ShopingCartList)
        {
            cartItem.Price = getPriceBasedOnQuantity(cartItem);
            ShoppingCartVm.OrderHeader.OrderTotal += (cartItem.Price * cartItem.Count);
        }

        return View(ShoppingCartVm);
    }

    public IActionResult Plus(int cartId)
    {
        var cartFromDb = _unitOfWork.ShopingCart.Get(u => u.Id == cartId);
        cartFromDb.Count += 1;
        _unitOfWork.ShopingCart.Update(cartFromDb);
        _unitOfWork.Save();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Minus(int cartId)
    {
        var cartFromDb = _unitOfWork.ShopingCart.Get(u => u.Id == cartId);
        if (cartFromDb.Count <= 0)
        {
            _unitOfWork.ShopingCart.Delete(cartFromDb);
        }
        else
        {
            cartFromDb.Count -= 1;
            _unitOfWork.ShopingCart.Update(cartFromDb);
        }

        _unitOfWork.Save();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Remove(int cartId)
    {
        var cartFromDb = _unitOfWork.ShopingCart.Get(u => u.Id == cartId);
        _unitOfWork.ShopingCart.Delete(cartFromDb);
        _unitOfWork.Save();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Summary()
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

        ShoppingCartVm = new()
        {
            ShopingCartList = _unitOfWork.ShopingCart.GetAll(u => u.ApplicationUserId == userId, include: "Product"),
            OrderHeader = new()
        };

        ShoppingCartVm.OrderHeader.ApplicationUser = _unitOfWork.AplicationUser.Get(u => u.Id == userId);

        ShoppingCartVm.OrderHeader.Name = ShoppingCartVm.OrderHeader.ApplicationUser.Name;
        ShoppingCartVm.OrderHeader.StreetAddress = ShoppingCartVm.OrderHeader.ApplicationUser.StreetAddress;
        ShoppingCartVm.OrderHeader.City = ShoppingCartVm.OrderHeader.ApplicationUser.City;
        ShoppingCartVm.OrderHeader.State = ShoppingCartVm.OrderHeader.ApplicationUser.State;
        ShoppingCartVm.OrderHeader.ZipCode = ShoppingCartVm.OrderHeader.ApplicationUser.ZipCode;

        foreach (var cartItem in ShoppingCartVm.ShopingCartList)
        {
            cartItem.Price = getPriceBasedOnQuantity(cartItem);
            ShoppingCartVm.OrderHeader.OrderTotal += (cartItem.Price * cartItem.Count);
        }

        return View(ShoppingCartVm);
    }

    [HttpPost]
    [ActionName("Summary")]
    public IActionResult SummaryPOST(ShoppingCartVm shoppingCartVm)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

        ShoppingCartVm.ShopingCartList =
            _unitOfWork.ShopingCart.GetAll(u => u.ApplicationUserId == userId, include: "Product");

        ShoppingCartVm.OrderHeader.OrderDate = System.DateTime.Now;
        ShoppingCartVm.OrderHeader.AplicationUserId = userId;


        ApplicationUser apliApplicationUser = _unitOfWork.AplicationUser.Get(u => u.Id == userId);
        
        foreach (var cartItem in ShoppingCartVm.ShopingCartList)
        {
            cartItem.Price = getPriceBasedOnQuantity(cartItem);
            ShoppingCartVm.OrderHeader.OrderTotal += (cartItem.Price * cartItem.Count);
        }

        if (apliApplicationUser.CompanyId.GetValueOrDefault() == 0)
        {
            ShoppingCartVm.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
            ShoppingCartVm.OrderHeader.OrderStatus = SD.StatusPending;
        }
        else
        {
            ShoppingCartVm.OrderHeader.PaymentStatus = SD.PaymentStatusDelayedPayment;
            ShoppingCartVm.OrderHeader.OrderStatus = SD.StatusApproved;
        }

        _unitOfWork.OrderHeader.Create(ShoppingCartVm.OrderHeader);
        _unitOfWork.Save();

        foreach (var cartItem in ShoppingCartVm.ShopingCartList)
        {
            OrderDetail orderDetail = new OrderDetail()
            {
                ProductId = cartItem.ProductId,
                OrderHeaderId = ShoppingCartVm.OrderHeader.Id,
                Price = cartItem.Price,
                Count = cartItem.Count,
            };
            _unitOfWork.OrderDetail.Create(orderDetail);
            _unitOfWork.Save();
        }
        if (apliApplicationUser.CompanyId.GetValueOrDefault() == 0)
        {
//TODO
        }


        return RedirectToAction(nameof(OrderConfirmation), new{id = ShoppingCartVm.OrderHeader.Id});
    }

    public IActionResult OrderConfirmation(int id)
    {
        return View(id);
    }

    private double getPriceBasedOnQuantity(ShopingCart shopingCart)
    {
        if (shopingCart.Count <= 50)
        {
            return shopingCart.Product.Price;
        }
        else
        {
            if (shopingCart.Count <= 100)
            {
                return shopingCart.Product.Price50;
            }
            else
            {
                return shopingCart.Product.Price100;
            }
        }
    }
}