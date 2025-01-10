using Udemy.DataAccess.Data;
using udemy.Models;

namespace udemy.Udemy.DataAccess.Repository;




public class ProductRepository : Repository<Product>, IProductRepository
{
    private AplicationDbContext _db;
    public ProductRepository(AplicationDbContext dbContext) : base(dbContext)
    {
        _db = dbContext;
    }

    public void Update(Product product)
    {
        _db.Products.Update(product);
    }

}