using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Data;

public class IdentityContext :IdentityDbContext
{
    public IdentityContext(DbContextOptions<IdentityContext> opts):base(opts)
    {

    }
}