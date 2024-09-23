using ConferenceRoomsWebAPI.DTO.Incoming;
using ConferenceRoomsWebAPI.DTO.Outcoming;
using ConferenceRoomsWebAPI.Entity;
using ConferenceRoomsWebAPI.Interfaces;

namespace ConferenceRoomsWebAPI.Services
{
    public class CompanyConferenceService : ICompanyConferenceService
    {
        private readonly ICompanyConferenceServiceRepository _service;

        public CompanyConferenceService(ICompanyConferenceServiceRepository service)
        {
            _service = service;
        }

        public async Task<List<CompanyServices>> GetAllCompanyServices()
        {
            return await _service.GetAllCompanyServices();
        }

        public async Task<CompanyServiceResponse> GetCompanyServiceId(int id)
        {
            var isExited = await _service.AnyCompanyServiceId(id);
            if (!isExited)
                throw new InvalidOperationException();

            var serviceId = await _service.GetCompanyService(id);

            return new CompanyServiceResponse
            {
                IdService = serviceId.IdService,
                ServiceName = serviceId.ServiceName,
                PriceService = serviceId.PriceService
            };
        }

        public async Task CreateCompanyService(CompanyServiceRequest service)
        {
            var isExited = await _service.AnyCompanyServiceName(service.ServiceName);
            if (isExited)
                throw new InvalidOperationException();

            await _service.CreateCompanyService(new CompanyServices
            {
                ServiceName = service.ServiceName,
                PriceService = service.PriceService
            });
        }

        public async Task DeleteCompanyService(int id)
        {
            var isExited = await _service.AnyCompanyServiceId(id);
            if (!isExited)
                throw new InvalidOperationException();

            await _service.DeleteCompanyServiceById(id);
        }

        public async Task<CompanyServiceResponse> UpdateCompanyService(int roomId, CompanyServiceRequest service)
        {
            var isExitedId = await _service.AnyCompanyServiceId(roomId);
            if (!isExitedId)
                throw new InvalidOperationException();

            var isExitedName = await _service.AnyCompanyServiceName(service.ServiceName);
            if (isExitedName)
                throw new InvalidOperationException();

            var newService = new CompanyServices
            {
                IdService = roomId,
                ServiceName = service.ServiceName,
                PriceService = service.PriceService
            };

            await _service.UpdateCompanyService(newService);
            CompanyServices companyServiceResponse = await _service.GetCompanyService(newService.IdService);
            return new CompanyServiceResponse
            {
                IdService = companyServiceResponse.IdService,
                ServiceName = service.ServiceName,
                PriceService = service.PriceService
            };
        }
    }
}
