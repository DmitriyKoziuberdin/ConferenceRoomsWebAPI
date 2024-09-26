using ConferenceRoomsWebAPI.DTO.Incoming;
using ConferenceRoomsWebAPI.DTO.Outcoming;
using ConferenceRoomsWebAPI.Entity;

namespace ConferenceRoomsWebAPI.Interfaces
{
    public interface ICompanyConferenceService
    {
        public Task<List<CompanyServices>> GetAllCompanyServicesAsync();
        public Task<CompanyServiceResponse> GetCompanyServiceByIdAsync(int id);
        public Task CreateCompanyServiceAsync(CompanyServiceRequest service);
        public Task<CompanyServiceResponse> UpdateCompanyServiceAsync(int roomId, CompanyServiceRequest service);
        public Task DeleteCompanyServiceByIdAsync(int id);
    }
}
