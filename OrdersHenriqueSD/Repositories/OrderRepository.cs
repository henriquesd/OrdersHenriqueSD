using Microsoft.EntityFrameworkCore;
using OrdersHenriqueSD.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersHenriqueSD.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrdersHenriqueSDContext _context;

        public OrderRepository(OrdersHenriqueSDContext context)
        {
            _context = context;
        }

        public List<Order> GetOrders()
        {
            var orders = from o in _context.Orders.Include(p => p.ProductOrderList)
                         select o;

            return orders.ToList();
        }

        public async Task Create(Order order)
        {
            await _context.AddAsync(order);

            foreach (var item in order.ProductOrderList)
            {
               item.OrderId = order.Id;
                await _context.ProductOrders.AddAsync(item);
            }

            await _context.SaveChangesAsync();
        }
    }
}
