
namespace PlatformService.Data
{
    public interface IPlatformRepository
    {
        bool SaveChanges();

        IEnumerable<Models.Platform> GetAllPlatforms();
        Models.Platform GetPlatformById(int id);
        void CreatePlatform(Models.Platform plat);
    }
}