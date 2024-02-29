
using Microsoft.EntityFrameworkCore;

namespace PlatformService.Data;

public static class PrepDb
{
    public static void PrepPopulation(IApplicationBuilder app, bool isProduction)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProduction);
        }
    }

    private static void SeedData(AppDbContext context, bool isProduction)
    {
        if (isProduction)
        {
            Console.WriteLine("---> Attempting to apply data migration...");
            try
            {
                context.Database.Migrate();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"---> Could not run migration: {exception.Message} ");
            }
        }
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
