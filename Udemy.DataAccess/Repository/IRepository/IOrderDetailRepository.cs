using udemy.Models;

namespace udemy.Udemy.DataAccess.Repository;

public interface IOrderDetailRepository : IRepository<OrderDetail>
{
    void Update(OrderDetail orderDetail);

}