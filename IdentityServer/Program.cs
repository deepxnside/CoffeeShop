using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var deafultConnString = builder.Configuration.GetConnectionString("DefaultConnection");

var assembly = typeof(Program).Assembly.GetName().Name;

builder.Services.AddIdentityServer().AddConfigurationStore(opts =>
    {
        opts.ConfigureDbContext = b =>

            b.UseNpgsql(deafultConnString,
                opt => opt.MigrationsAssembly(assembly));

    }
).AddOperationalStore(opts =>
    {
        opts.ConfigureDbContext = b =>

            b.UseNpgsql(deafultConnString,
                opt => opt.MigrationsAssembly(assembly));

    }

).AddDeveloperSigningCredential();


var app = builder.Build();


app.Run();