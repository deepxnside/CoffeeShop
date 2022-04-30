using CoffeeShop.Data;
using CoffeeShop.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Services;

public class CoffeeShopService :ICoffeeShopService
{
    private readonly AppDbContext _context;

    public CoffeeShopService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CoffeeShopModel>> CoffeeList()
    {
        
         return await _context.CoffeeShops.ToListAsync();
           
    }

    public async Task<CoffeeShopModel> Coffee(int id)
    {
        return await _context.CoffeeShops.FirstOrDefaultAsync(c => c.Id == id);
    }
}