using CoffeeShop.Models;
using CoffeeShop.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Controllers;

[Route("api/coffee")]
[ApiController]
public class CoffeeController : ControllerBase
{
    private readonly ICoffeeShopService _repository;

    public CoffeeController(ICoffeeShopService repository)
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