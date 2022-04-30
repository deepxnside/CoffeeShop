using CoffeeShop.Models;

namespace CoffeeShop.Services;

public interface ICoffeeShopService
{
    Task<IEnumerable<CoffeeShopModel>> CoffeeList();
    Task<CoffeeShopModel> Coffee(int id);
}