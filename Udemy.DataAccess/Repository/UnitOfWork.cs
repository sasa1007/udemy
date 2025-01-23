using Udemy.DataAccess.Data;

namespace udemy.Udemy.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    private AplicationDbContext _db;

    public ICategoryRepository Category { get; private set; }
    public IProductRepository Product { get; private set; }
    public ICompanyRepository Company { get; private set; }

    public UnitOfWork(AplicationDbContext dbContext)
    {
        _db = dbContext;
        Category = new CategoryRepository(_db);
        Product = new ProductRepository(_db);
        Company = new CompanyRepository(_db);
    }


    public void Save()
    {
        _db.SaveChanges();
    }
}