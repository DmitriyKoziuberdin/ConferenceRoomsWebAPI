using ConferenceRoomsWebAPI.DTO.Incoming;
using ConferenceRoomsWebAPI.DTO.Outcoming;
using ConferenceRoomsWebAPI.Entity;
using ConferenceRoomsWebAPI.Interfaces;

namespace ConferenceRoomsWebAPI.Services
{
    public class CompanyConferenceService : ICompanyConferenceService
    {
        private readonly ICompanyConferenceServiceRepository _companyServiceRepository;

        public CompanyConferenceService(ICompanyConferenceServiceRepository companyServiceRepository)
        {
            _companyServiceRepository = companyServiceRepository;
        }

        public async Task<List<CompanyServices>> GetAllCompanyServices()
        {
            return await _companyServiceRepository.GetAllCompanyServices();
        }

        public async Task<CompanyServiceResponse> GetCompanyServiceId(int id)
        {
            var isExited = await _companyServiceRepository.AnyCompanyServiceId(id);
            if (!isExited)
                throw new InvalidOperationException();

            var serviceId = await _companyServiceRepository.GetCompanyService(id);

            return new CompanyServiceResponse
            {
                IdService = serviceId.IdService,
                ServiceName = serviceId.ServiceName,
                PriceService = serviceId.PriceService
            };
        }

        public async Task CreateCompanyService(CompanyServiceRequest service)
        {
            var isExited = await _companyServiceRepository.AnyCompanyServiceName(service.ServiceName);
            if (isExited)
                throw new InvalidOperationException();

            await _companyServiceRepository.CreateCompanyService(new CompanyServices
            {
                ServiceName = service.ServiceName,
                PriceService = service.PriceService
            });
        }

        public async Task DeleteCompanyService(int id)
        {
            var isExited = await _companyServiceRepository.AnyCompanyServiceId(id);
            if (!isExited)
                throw new InvalidOperationException();

            await _companyServiceRepository.DeleteCompanyServiceById(id);
        }

        public async Task<CompanyServiceResponse> UpdateCompanyService(int roomId, CompanyServiceRequest service)
        {
            var isExitedId = await _companyServiceRepository.AnyCompanyServiceId(roomId);
            if (!isExitedId)
                throw new InvalidOperationException();

            var isExitedName = await _companyServiceRepository.AnyCompanyServiceName(service.ServiceName);
            if (isExitedName)
                throw new InvalidOperationException();

            var newService = new CompanyServices
            {
                IdService = roomId,
                ServiceName = service.ServiceName,
                PriceService = service.PriceService
            };

            await _companyServiceRepository.UpdateCompanyService(newService);
            CompanyServices companyServiceResponse = await _companyServiceRepository.GetCompanyService(newService.IdService);
            return new CompanyServiceResponse
            {
                IdService = companyServiceResponse.IdService,
                ServiceName = service.ServiceName,
                PriceService = service.PriceService
            };
        }
    }
}
