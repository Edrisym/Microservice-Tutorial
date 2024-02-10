using Microsoft.EntityFrameworkCore;

namespace PlatformService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Models.Platform> Platforms { get; set; }

}
