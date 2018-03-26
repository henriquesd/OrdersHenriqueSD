using Microsoft.EntityFrameworkCore;

namespace OrdersHenriqueSD.Models
{
    public class OrdersHenriqueSDContext : DbContext
    {
        public OrdersHenriqueSDContext(DbContextOptions<OrdersHenriqueSDContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<UserPortal> UsersPortal { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }

    }
}
