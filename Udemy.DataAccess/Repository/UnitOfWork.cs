using Udemy.DataAccess.Data;

namespace udemy.Udemy.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    private AplicationDbContext _db;

    public ICategoryRepository Category { get; private set; }
    public IProductRepository Product { get; private set; }
    public ICompanyRepository Company { get; private set; }
    public IShopingCartRepository ShopingCart { get; private set; }
    public IAplicationUserRepository AplicationUser { get; private set; }

    public IOrderHeaderRepository OrderHeader { get; private set; }
    public IOrderDetailRepository OrderDetail { get; private set; }

    public UnitOfWork(AplicationDbContext dbContext)
    {
        _db = dbContext;
        Category = new CategoryRepository(_db);
        Product = new ProductRepository(_db);
        Company = new CompanyRepository(_db);
        ShopingCart = new ShopingCartRepository(_db);
        AplicationUser = new AplicationUserRepository(_db);
        OrderHeader = new OrderHeaderRepository(_db);
        OrderDetail = new OrderDetailRepository(_db);
    }


    public void Save()
    {
        _db.SaveChanges();
    }
}