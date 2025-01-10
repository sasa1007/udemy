using udemy.Models;

namespace udemy.Udemy.DataAccess.Repository;

public interface IProductRepository : IRepository<Product>
{
    void Update(Product product);

}