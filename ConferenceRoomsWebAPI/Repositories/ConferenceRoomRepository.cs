using ConferenceRoomsWebAPI.ApplicationDb;
using ConferenceRoomsWebAPI.Interfaces.IRepository;

namespace ConferenceRoomsWebAPI.Repositories
{
    public class ConferenceRoomRepository : IConferenceRoomRepository
    {
        private readonly ApplicationDbContext _context;

        public ConferenceRoomRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
