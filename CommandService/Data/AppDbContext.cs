using CommandService.Models;
using CommandsService.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> option) : base(option)
    {

    }

    public DbSet<Platform> Platforms { get; set; }
    public DbSet<Command> Commands { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Platform>()
        .HasMany(x => x.Commands)
        .WithOne(x => x.Platform!)
        .HasForeignKey(x => x.PlatformId);

        modelBuilder.Entity<Command>()
        .HasOne(x => x.Platform)
        .WithMany(x => x.Commands)
        .HasForeignKey(x => x.PlatformId);

    }
}
