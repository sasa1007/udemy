using Udemy.DataAccess.Data;
using udemy.Models;

namespace udemy.Udemy.DataAccess.Repository;




public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
{
    private AplicationDbContext _db;
    public OrderHeaderRepository(AplicationDbContext dbContext) : base(dbContext)
    {
        _db = dbContext;
    }

    public void Update(OrderHeader orderHeader)
    {
        _db.OrderHeaders.Update(orderHeader);
    }

}