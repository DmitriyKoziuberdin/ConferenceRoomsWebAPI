using ConferenceRoomsWebAPI.ApplicationDb;
using ConferenceRoomsWebAPI.Entity;
using ConferenceRoomsWebAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomsWebAPI.Repositories
{
    public class ConferenceRoomRepository : IConferenceRoomRepository
    {
        private readonly ApplicationDbContext _context;

        public ConferenceRoomRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ConferenceRooms>> GetAllConferenceRooms()
        {
            return await _context.ConferenceRooms.ToListAsync();
        }

        public async Task<ConferenceRooms> GetConferenceRoom(int id)
        {
            return await _context.ConferenceRooms
                .FirstAsync(roomId => roomId.IdRoom == id);
        }

        public async Task CreateConferenceRoom(ConferenceRooms room)
        {
            await _context.ConferenceRooms.AddAsync(room);
            await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteConferenceRoomById(int id)
        {
            var deletingRoom = await _context.ConferenceRooms
                .Where(roomId => roomId.IdRoom == id)
                .ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
            return deletingRoom;
        }

        public async Task UpdateConferenceRoom(ConferenceRooms room)
        {
            _context.ConferenceRooms.Update(room);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AnyConferenceRoomId(int id)
        {
            return await _context.ConferenceRooms
                .AnyAsync(roomId => roomId.IdRoom == id);
        }

        public async Task<bool> AnyConferenceRoomName(string name)
        {
            return await _context.ConferenceRooms
                .AnyAsync(roomName => roomName.NameRoom == name);
        }
    }
}
