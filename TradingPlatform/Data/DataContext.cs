using Microsoft.EntityFrameworkCore;

namespace TradingPlatform.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<user_info> User_Infos { get; set; }
        public DbSet<user_stock> User_Stocks { get; set; }
        public DbSet<user_order> User_Orders { get; set; }
    }
}
