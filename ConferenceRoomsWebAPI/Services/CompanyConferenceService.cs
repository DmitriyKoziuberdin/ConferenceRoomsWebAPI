using ConferenceRoomsWebAPI.ApplicationDb;
using ConferenceRoomsWebAPI.Interfaces;

namespace ConferenceRoomsWebAPI.Services
{
    public class CompanyConferenceService : ICompanyConferenceService
    {
        private readonly ApplicationDbContext _context;

        public CompanyConferenceService(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
