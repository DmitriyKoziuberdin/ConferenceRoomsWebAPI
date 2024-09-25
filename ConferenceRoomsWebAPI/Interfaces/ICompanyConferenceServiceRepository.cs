using ConferenceRoomsWebAPI.Entity;

namespace ConferenceRoomsWebAPI.Interfaces
{
    public interface ICompanyConferenceServiceRepository
    {
        public Task<List<CompanyServices>> GetAllCompanyServices();
        public Task<CompanyServices> GetCompanyServiceById(int id);
        public Task CreateCompanyService(CompanyServices room);
        public Task UpdateCompanyService(CompanyServices room);
        public Task<int> DeleteCompanyServiceById(int id);
        public Task<bool> AnyCompanyServiceId(int id);
        public Task<bool> AnyCompanyServiceName(string name);
    }
}
