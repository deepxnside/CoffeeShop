using CoffeeShop.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Data;
public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opts): base(opts)
    {
        
    }
    
    public DbSet<CoffeeShopModel> CoffeeShops { get; set; }
}