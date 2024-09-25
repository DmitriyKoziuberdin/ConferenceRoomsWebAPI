using ConferenceRoomsWebAPI.Entity;

namespace ConferenceRoomsWebAPI.Interfaces
{
    public interface ICompanyConferenceServiceRepository
    {
        public Task<List<CompanyServices>> GetAllCompanyServicesAsync();
        public Task<CompanyServices> GetCompanyServiceByIdAsync(int id);
        public Task CreateCompanyServiceAsync(CompanyServices room);
        public Task UpdateCompanyServiceAsync(CompanyServices room);
        public Task<int> DeleteCompanyServiceByIdAsync(int id);
        public Task<bool> AnyCompanyServiceIdAsync(int id);
        public Task<bool> AnyCompanyServiceNameAsync(string name);
    }
}
