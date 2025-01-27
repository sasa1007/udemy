namespace udemy.Udemy.DataAccess.Repository;

public interface IUnitOfWork
{
    ICategoryRepository Category { get; }
    IProductRepository Product { get; }
    ICompanyRepository Company { get; }
    IShopingCartRepository ShopingCart { get; }
    IAplicationUserRepository AplicationUser { get; }
    IOrderDetailRepository OrderDetail { get; }
    IOrderHeaderRepository OrderHeader { get; }

    void Save();
}