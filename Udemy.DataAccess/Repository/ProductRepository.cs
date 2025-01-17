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
        var productFromDB = _db.Products.FirstOrDefault(x => x.Id == product.Id);
        if (productFromDB != null)
        {
            productFromDB.Name = product.Name;
            productFromDB.Isbn = product.Isbn;
            productFromDB.Price = product.Price;
            productFromDB.Price50 = product.Price50;
            productFromDB.ListPrice = product.ListPrice;
            productFromDB.Price100 = product.Price100;
            productFromDB.Description = product.Description;
            productFromDB.CategoryId = product.CategoryId;
            productFromDB.Author = product.Author;
            if (product.ImageUrl != null)
            {
                productFromDB.ImageUrl = product.ImageUrl;
            }
        }
        // _db.Products.Update(product);
    }

}