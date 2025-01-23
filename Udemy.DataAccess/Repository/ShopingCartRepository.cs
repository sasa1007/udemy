using Udemy.DataAccess.Data;
using udemy.Models;

namespace udemy.Udemy.DataAccess.Repository;




public class ShopingCartRepository : Repository<ShopingCart>, IShopingCartRepository
{
    private AplicationDbContext _db;
    public ShopingCartRepository(AplicationDbContext dbContext) : base(dbContext)
    {
        _db = dbContext;
    }

    public void Update(ShopingCart shopingCart)
    {
        _db.ShopingCarts.Update(shopingCart);
    }

}