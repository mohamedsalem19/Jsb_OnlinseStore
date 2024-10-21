
using OnlineStore.Entities;

namespace OnlineStore
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {

        }

        public DbSet<Products> Products { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrdersProducts> OrdersProducts { get; set; }
    }
}
