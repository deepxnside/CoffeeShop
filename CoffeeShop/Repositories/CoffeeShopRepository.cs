using CoffeeShop.Data;
using CoffeeShop.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Repositories;

public class CoffeeShopRepository :ICoffeeShopRepository
{
    private readonly AppDbContext _context;

    public CoffeeShopRepository(AppDbContext context)
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