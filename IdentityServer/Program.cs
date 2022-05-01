using IdentityServer;
using IdentityServer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var seed = args.Contains("/seed");

if (seed)
{
    args = args.Except(new[] {"/seed"}).ToArray();
}

var builder = WebApplication.CreateBuilder(args);



var deafultConnString = builder.Configuration.GetConnectionString("DefaultConnection");

var assembly = typeof(Program).Assembly.GetName().Name;

if (seed)
{
    SeedData.EnsureSeedData(deafultConnString);
}
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
app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();
app.UseEndpoints(end =>
    end.MapDefaultControllerRoute());
app.Run();