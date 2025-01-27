using udemy.Models;

namespace udemy.Udemy.DataAccess.Repository;

public interface IOrderHeaderRepository : IRepository<OrderHeader>
{
    void Update(OrderHeader orderHeader);

}