using udemy.Models;

namespace udemy.Udemy.DataAccess.Repository;

public interface IShopingCartRepository: IRepository<ShopingCart>
{
    void Update(ShopingCart shopingCart);
}