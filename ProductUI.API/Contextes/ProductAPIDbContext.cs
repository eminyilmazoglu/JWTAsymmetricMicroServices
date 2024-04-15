using Microsoft.EntityFrameworkCore;
using ProductUI.API.Models;

namespace ProductUI.API.Contextes
{
    public class ProductAPIDbContext : DbContext
    {
        public ProductAPIDbContext(DbContextOptions options) : base(options)
        {
                
        }

        public DbSet<Product> Products { get; set; }
    }
}
