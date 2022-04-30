using CoffeeShop.Data;
using CoffeeShop.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<ICoffeeShopService,CoffeeShopService>();

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


app.Run();