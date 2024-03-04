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
}
