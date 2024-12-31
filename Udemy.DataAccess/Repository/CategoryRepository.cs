using Udemy.DataAccess.Data;
using udemy.Models;

namespace udemy.Udemy.DataAccess.Repository;




public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private AplicationDbContext _db;

    public CategoryRepository(AplicationDbContext dbContext) : base(dbContext)
    {
        _db = dbContext;
    }

    public void Update(Category category)
    {
        _db.Categories.Update(category);
    }

    public void Save()
    {
        _db.SaveChanges();
    }
}