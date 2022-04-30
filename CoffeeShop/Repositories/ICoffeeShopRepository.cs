using CoffeeShop.Models;

namespace CoffeeShop.Repositories;

public interface ICoffeeShopRepository
{
    Task<IEnumerable<CoffeeShopModel>> CoffeeList();
    Task<CoffeeShopModel> Coffee(int id);
}