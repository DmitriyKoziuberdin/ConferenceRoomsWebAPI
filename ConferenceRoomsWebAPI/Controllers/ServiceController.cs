using ConferenceRoomsWebAPI.DTO.Incoming;
using ConferenceRoomsWebAPI.DTO.Outcoming;
using ConferenceRoomsWebAPI.Entity;
using ConferenceRoomsWebAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomsWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly ICompanyConferenceService _service;

        public ServiceController(ICompanyConferenceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<List<CompanyServices>> GetAllCompanyServices()
        {
            return await _service.GetAllCompanyServicesAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<CompanyServiceResponse> GetCompanyServiceId([FromRoute] int id)
        {
            return await _service.GetCompanyServiceByIdAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompanyService([FromBody] CompanyServiceRequest service)
        {
            await _service.CreateCompanyServiceAsync(service);
            return Ok();
        }

        [HttpPut("{serviceId:int}/")]
        public async Task<ActionResult<CompanyServiceResponse>> UpdateCompanyService([FromRoute] int serviceId, [FromBody] CompanyServiceRequest service)
        {
            return new OkObjectResult(await _service.UpdateCompanyServiceAsync(serviceId, service));
        }

        [HttpDelete("{serviceId:int}")]
        public async Task<IActionResult> DeleteCompanyService([FromRoute] int serviceId)
        {
            await _service.DeleteCompanyServiceByIdAsync(serviceId);
            return Ok();
        }
    }
}
