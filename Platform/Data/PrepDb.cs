
using PlatformService.Models;

namespace PlatformService.Data;

public static class PrepDb
{
    public static void PrepPopulation(IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
        }
    }

    private static void SeedData(AppDbContext context)
    {
        if (!context.Platforms.Any())
        {
            Console.WriteLine("---> Seeding database...");
            context.Platforms.AddRange(
           new Models.Platform() { Name = "dotnet", Publisher = "Microsoft", Cost = "Free" },
           new Models.Platform() { Name = "Edris", Publisher = "Bluh Bluh Bluh", Cost = "Expensive" });

            context.SaveChanges();
        }
        else
        {
            Console.WriteLine("---> We already have database");
        }
    }
}
