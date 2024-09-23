using ConferenceRoomsWebAPI.DTO.Incoming;
using ConferenceRoomsWebAPI.DTO.Outcoming;
using ConferenceRoomsWebAPI.Entity;

namespace ConferenceRoomsWebAPI.Interfaces
{
    public interface ICompanyConferenceService
    {
        public Task<List<CompanyServices>> GetAllCompanyServices();
        public Task<CompanyServiceResponse> GetCompanyServiceId(int id);
        public Task CreateCompanyService(CompanyServiceRequest service);
        public Task<CompanyServiceResponse> UpdateCompanyService(int roomId, CompanyServiceRequest service);
        public Task DeleteCompanyService(int id);
    }
}
