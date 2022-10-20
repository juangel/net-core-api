using CoreApiTest.API.Models.Domain;

namespace CoreApiTest.API.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();
    }
}
