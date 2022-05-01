using System.Formats.Asn1;
using System.Security.Claims;
using IdentityModel;
using IdentityServer.Data;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.EntityFramework.Storage;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace IdentityServer;

public class SeedData
{
    public static void EnsureSeedData(String connString)
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddDbContext<IdentityContext>(
            opts => opts.UseNpgsql(connString));

        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders();
        services.AddOperationalDbContext(
            opts =>
            {
                opts.ConfigureDbContext = db =>
                    db.UseNpgsql(
                        connString,
                        sql =>
                            sql.MigrationsAssembly(typeof(SeedData).Assembly.FullName));

            }).AddConfigurationDbContext(
            opts =>
            {
                opts.ConfigureDbContext = db =>
                    db.UseNpgsql(
                        connString,
                        sql =>
                            sql.MigrationsAssembly(typeof(SeedData).Assembly.FullName));
            });

        var serviceProvider = services.BuildServiceProvider();

        var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        scope.ServiceProvider.GetService<PersistedGrantDbContext>().Database.Migrate();

        var context=scope.ServiceProvider.GetService<ConfigurationDbContext>();
        context.Database.Migrate();
        
        EnsureSeedData(context);

        var ctx = scope.ServiceProvider.GetService<IdentityContext>();
        ctx.Database.Migrate();
        EnsureUsers(scope);
    }

    private static void EnsureSeedData(ConfigurationDbContext context)
    {
        if (!context.Clients.Any())
        {
            foreach (var client in Config.Clients.ToList())
            {
                context.Clients.Add(client.ToEntity());
            }

            context.SaveChanges();
        }
        
        if (!context.IdentityResources.Any())
        {
            foreach (var resource in Config.IdentityResource.ToList())
            {
                context.IdentityResources.Add(resource.ToEntity());
            }

            context.SaveChanges();
        }
        
        if (!context.ApiResources.Any())
        {
            foreach (var resource in Config.ApiResources.ToList())
            {
                context.ApiResources.Add(resource.ToEntity());
            }

            context.SaveChanges();
        }
        
        if (!context.ApiScopes.Any())
        {
            foreach (var scope in Config.ApiScopes.ToList())
            {
                context.ApiScopes.Add(scope.ToEntity());
            }

            context.SaveChanges();
        }
        
    }

    private static void EnsureUsers(IServiceScope scope)
    {
        var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

        var angella = userMgr.FindByNameAsync("angella").Result;
        if (angella == null)
        {
            angella = new IdentityUser
            {
                UserName = "angella",
                Email = "angella.freeman@email.com",
                EmailConfirmed = true
            };
            var result = userMgr.CreateAsync(angella, "Password123!@#").Result;

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = userMgr.AddClaimsAsync(
                angella,
                new Claim[]
                {
                    new Claim(JwtClaimTypes.Name,"Angella Freeman"),
                    new Claim(JwtClaimTypes.GivenName,"Angella"),
                    new Claim(JwtClaimTypes.FamilyName,"Freeman"),
                    new Claim(JwtClaimTypes.WebSite,"angellafreeman.com"),
                    new Claim("location","somewhere"),
                }).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
        }
    }
}