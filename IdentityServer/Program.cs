using IdentityServer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var deafultConnString = builder.Configuration.GetConnectionString("DefaultConnection");

var assembly = typeof(Program).Assembly.GetName().Name;

builder.Services.AddDbContext<IdentityContext>(opts =>
        opts.UseNpgsql(deafultConnString, opt => opt.MigrationsAssembly(assembly)
));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityContext>();

builder.Services.AddIdentityServer()
    .AddAspNetIdentity<IdentityUser>()
    .AddConfigurationStore(opts =>
    {
        opts.ConfigureDbContext = b =>
        b.UseNpgsql(deafultConnString, opt => opt.MigrationsAssembly(assembly));
    }).AddOperationalStore(opts =>
{
    opts.ConfigureDbContext = b =>
    b.UseNpgsql(deafultConnString, opt => opt.MigrationsAssembly(assembly));
})
    .AddDeveloperSigningCredential();



var app = builder.Build();

app.UseIdentityServer();

app.Run();