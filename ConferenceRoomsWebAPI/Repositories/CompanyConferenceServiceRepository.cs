using ConferenceRoomsWebAPI.ApplicationDb;
using ConferenceRoomsWebAPI.Entity;
using ConferenceRoomsWebAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomsWebAPI.Repositories
{
    public class CompanyConferenceServiceRepository : ICompanyConferenceServiceRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyConferenceServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CompanyServices>> GetAllCompanyServices()
        {
            return await _context.CompanyServices.ToListAsync();
        }

        public async Task<CompanyServices> GetCompanyService(int id)
        {
            return await _context.CompanyServices
                .FirstAsync(serviceId => serviceId.IdService == id);
        }

        public async Task CreateCompanyService(CompanyServices service)
        {
            await _context.CompanyServices.AddAsync(service);
            await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteCompanyServiceById(int id)
        {
            var deletingService = await _context.CompanyServices
                .Where(serviceId => serviceId.IdService == id)
                .ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
            return deletingService;
        }

        public async Task UpdateCompanyService(CompanyServices service)
        {
            _context.CompanyServices.Update(service);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AnyCompanyServiceId(int id)
        {
            return await _context.CompanyServices
                .AnyAsync(serviceId => serviceId.IdService == id);
        }

        public async Task<bool> AnyCompanyServiceName(string name)
        {
            return await _context.CompanyServices
                .AnyAsync(serviceName => serviceName.ServiceName == name);
        }
    }
}
