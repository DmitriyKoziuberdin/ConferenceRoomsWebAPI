using Common.Exceptions;
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

        public async Task<List<CompanyServices>> GetAllCompanyServicesAsync()
        {
            return await _companyServiceRepository.GetAllCompanyServicesAsync();
        }

        public async Task<CompanyServiceResponse> GetCompanyServiceByIdAsync(int id)
        {
            var isExited = await _companyServiceRepository.AnyCompanyServiceIdAsync(id);
            if (!isExited)
                throw new CompanyServiceNotFoundException($"Service with this ID: {id} not found.");

            var serviceId = await _companyServiceRepository.GetCompanyServiceByIdAsync(id);

            return new CompanyServiceResponse
            {
                IdService = serviceId.IdService,
                ServiceName = serviceId.ServiceName,
                PriceService = serviceId.PriceService
            };
        }

        public async Task CreateCompanyServiceAsync(CompanyServiceRequest service)
        {
            var isExited = await _companyServiceRepository.AnyCompanyServiceNameAsync(service.ServiceName);
            if (isExited)
                throw new CompanyServiceDuplicateNameException($"Service with this name: {service.ServiceName} already use.");

            await _companyServiceRepository.CreateCompanyServiceAsync(new CompanyServices
            {
                ServiceName = service.ServiceName,
                PriceService = service.PriceService
            });
        }

        public async Task DeleteCompanyServiceByIdAsync(int id)
        {
            var isExited = await _companyServiceRepository.AnyCompanyServiceIdAsync(id);
            if (!isExited)
                throw new CompanyServiceNotFoundException($"Service with this ID: {id} not found.");

            await _companyServiceRepository.DeleteCompanyServiceByIdAsync(id);
        }

        public async Task<CompanyServiceResponse> UpdateCompanyServiceAsync(int roomId, CompanyServiceRequest service)
        {
            var isExitedId = await _companyServiceRepository.AnyCompanyServiceIdAsync(roomId);
            if (!isExitedId)
                throw new CompanyServiceNotFoundException($"Service with this ID: {roomId} not found.");

            var isExitedName = await _companyServiceRepository.AnyCompanyServiceNameAsync(service.ServiceName);
            if (isExitedName)
                if (isExitedName)
                    throw new CompanyServiceDuplicateNameException($"Service with this name: {service.ServiceName} already use.");

            var newService = new CompanyServices
            {
                IdService = roomId,
                ServiceName = service.ServiceName,
                PriceService = service.PriceService
            };

            await _companyServiceRepository.UpdateCompanyServiceAsync(newService);
            CompanyServices companyServiceResponse = await _companyServiceRepository.GetCompanyServiceByIdAsync(newService.IdService);
            return new CompanyServiceResponse
            {
                IdService = companyServiceResponse.IdService,
                ServiceName = service.ServiceName,
                PriceService = service.PriceService
            };
        }
    }
}
