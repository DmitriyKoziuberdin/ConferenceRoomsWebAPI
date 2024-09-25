using ConferenceRoomsWebAPI.Entity;
using ConferenceRoomsWebAPI.Interfaces;
using ConferenceRoomsWebAPI.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace ConferenceRoomsWebAPI.CachedRepositories
{
    public class CachedCompanyConferenceServiceRepository : ICompanyConferenceServiceRepository
    {
        private readonly CompanyConferenceServiceRepository _companyConferenceServiceRepository;
        private readonly IMemoryCache _memoryCache;
        private static string _cacheKey = "service";

        public CachedCompanyConferenceServiceRepository(CompanyConferenceServiceRepository companyConferenceServiceRepository, IMemoryCache memoryCache)
        {
            _companyConferenceServiceRepository = companyConferenceServiceRepository;
            _memoryCache = memoryCache;
        }

        public async Task<List<CompanyServices>> GetAllCompanyServices()
        {
            var service = await _memoryCache
               .GetOrCreateAsync(_cacheKey, (entry) => _companyConferenceServiceRepository.GetAllCompanyServices());
            return service!.ToList();
        }

        public async Task<CompanyServices> GetCompanyServiceById(int id)
        {
            return await _companyConferenceServiceRepository.GetCompanyServiceById(id);
        }

        public async Task CreateCompanyService(CompanyServices room)
        {
            _memoryCache.Remove(_cacheKey);
             await _companyConferenceServiceRepository.CreateCompanyService(room);
        }

        public async Task UpdateCompanyService(CompanyServices room)
        {
            _memoryCache.Remove(_cacheKey);
            await _companyConferenceServiceRepository.UpdateCompanyService(room);
        }

        public async Task<int> DeleteCompanyServiceById(int id)
        {
            _memoryCache.Remove(_cacheKey);
            return await _companyConferenceServiceRepository.DeleteCompanyServiceById(id);
        }

        public async Task<bool> AnyCompanyServiceId(int id)
        {
            return await _companyConferenceServiceRepository.AnyCompanyServiceId(id);
        }

        public async Task<bool> AnyCompanyServiceName(string name)
        {
            return await _companyConferenceServiceRepository.AnyCompanyServiceName(name);
        }
    }
}
