using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using proekt.Models;
using proekt.Data.DataAccess.Interfaces;

namespace proekt.Data.DataAccess.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        // Пример метода для получения заказов пользователя
        public IEnumerable<Order> GetOrdersByUserId(string userId)
        {
            return _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.UserId == userId)
                .ToList();
        }

        // Пример метода для получения активных заказов
        public IEnumerable<Order> GetActiveOrders()
        {
            return _context.Orders
                .Where(o => o.Status == "Active")
                .ToList();
        }
    }
}
