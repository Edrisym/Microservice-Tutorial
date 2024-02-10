
using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepository : IPlatformRepository
    {
        private readonly AppDbContext _context;

        public PlatformRepository(AppDbContext context)
        {
            _context = context;
        }

        public void CreatePlatform(Models.Platform platfrom)
        {
            if (platfrom == null)
            {
                throw new ArgumentNullException(nameof(platfrom));
            }

            _context.Platforms.Add(platfrom);
        }

        public IEnumerable<PlatformService.Models.Platform> GetAllPlatforms()
        {
            return _context.Platforms.ToList();
        }

        public PlatformService.Models.Platform GetPlatformById(int id)
        {
            return _context.Platforms.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}