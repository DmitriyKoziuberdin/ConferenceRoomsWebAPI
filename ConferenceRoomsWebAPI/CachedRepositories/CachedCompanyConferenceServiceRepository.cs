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

        public async Task<List<CompanyServices>> GetAllCompanyServicesAsync()
        {
            var service = await _memoryCache
               .GetOrCreateAsync(_cacheKey, (entry) => _companyConferenceServiceRepository.GetAllCompanyServicesAsync());
            return service!.ToList();
        }

        public async Task<CompanyServices> GetCompanyServiceByIdAsync(int id)
        {
            return await _companyConferenceServiceRepository.GetCompanyServiceByIdAsync(id);
        }

        public async Task CreateCompanyServiceAsync(CompanyServices room)
        {
            _memoryCache.Remove(_cacheKey);
             await _companyConferenceServiceRepository.CreateCompanyServiceAsync(room);
        }

        public async Task UpdateCompanyServiceAsync(CompanyServices room)
        {
            _memoryCache.Remove(_cacheKey);
            await _companyConferenceServiceRepository.UpdateCompanyServiceAsync(room);
        }

        public async Task<int> DeleteCompanyServiceByIdAsync(int id)
        {
            _memoryCache.Remove(_cacheKey);
            return await _companyConferenceServiceRepository.DeleteCompanyServiceByIdAsync(id);
        }

        public async Task<bool> AnyCompanyServiceIdAsync(int id)
        {
            return await _companyConferenceServiceRepository.AnyCompanyServiceIdAsync(id);
        }

        public async Task<bool> AnyCompanyServiceNameAsync(string name)
        {
            return await _companyConferenceServiceRepository.AnyCompanyServiceNameAsync(name);
        }
    }
}
