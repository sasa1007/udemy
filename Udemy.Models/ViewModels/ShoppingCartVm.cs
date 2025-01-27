using System.ComponentModel.DataAnnotations.Schema;

namespace udemy.Models.ViewModels;

public class ShoppingCartVm
{
    public IEnumerable<ShopingCart> ShopingCartList { get; set; }
    public OrderHeader OrderHeader { get; set; }

}