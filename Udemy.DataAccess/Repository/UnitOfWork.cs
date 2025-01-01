using Udemy.DataAccess.Data;

namespace udemy.Udemy.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    private AplicationDbContext _db;

    public ICategoryRepository Category { get; private set; }

    public UnitOfWork(AplicationDbContext dbContext)
    {
        _db = dbContext;
        Category = new CategoryRepository(_db);
    }




    public void Save()
    {
        _db.SaveChanges();
    }
}