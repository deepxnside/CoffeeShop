using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Controllers;

[ApiController]
[Route("test")]
public class TestController : ControllerBase
{
    public string Get()
    {
        return "Returning from TestController Get Method";
    }
}