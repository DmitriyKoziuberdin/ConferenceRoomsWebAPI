using ConferenceRoomsWebAPI.ApplicationDb;
using ConferenceRoomsWebAPI.Interfaces.IService;

namespace ConferenceRoomsWebAPI.Services
{
    public class ConferenceRoomService : IConferenceRoomservice
    {
        private readonly ApplicationDbContext _context; 

        public ConferenceRoomService(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
