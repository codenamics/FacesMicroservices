using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrdersApi.Models;

namespace OrdersApi.Persistence
{
    public class OrderContext :DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options): base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            var converter = new EnumToStringConverter<Status>();
            builder.Entity<Order>().Property(x => x.Status).HasConversion(converter);
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
