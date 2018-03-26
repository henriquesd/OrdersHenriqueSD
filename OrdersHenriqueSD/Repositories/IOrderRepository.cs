using OrdersHenriqueSD.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrdersHenriqueSD.Repositories
{
    public interface IOrderRepository
    {
        List<Order> GetOrders();
        Task Create(Order order);
    }
}
