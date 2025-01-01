using udemy.Models;

namespace udemy.Udemy.DataAccess.Repository;

public interface ICategoryRepository : IRepository<Category>
{
    void Update(Category category);

}