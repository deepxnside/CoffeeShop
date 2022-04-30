using CoffeeShop.Models;
using CoffeeShop.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Controllers;

[Route("api/coffee")]
[ApiController]
public class CoffeeController : ControllerBase
{
    private readonly ICoffeeShopRepository _repository;

    public CoffeeController(ICoffeeShopRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CoffeeShopModel>>> CoffeeList()
    {
        return Ok(await _repository.CoffeeList());
    }

    [HttpGet("{id}")]
    public async Task<CoffeeShopModel> Coffee(int id)
    {
        return await _repository.Coffee(id);
    }
}