using proekt.Models;

namespace proekt.Data.DataAccess.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<Order> GetOrdersByUserId(string userId);
        IEnumerable<Order> GetActiveOrders();
    }
}
