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

        public async Task<List<ConferenceRooms>> GetAllConferenceRoomsAsync()
        {
            return await _context.ConferenceRooms.ToListAsync();
        }

        //public async Task<ConferenceRooms> GetConferenceRoomId(int id)
        //{
        //    return await _context.ConferenceRooms
        //        .Include(cs => cs.CompanyServices)
        //        .FirstAsync(roomId => roomId.IdRoom == id);
        //}

        public async Task<ConferenceRooms> GetConferenceRoomByIdAsync(int id)
        {
            return await _context.ConferenceRooms
                .Include(cs => cs.CompanyServices)
                .FirstAsync(roomId => roomId.IdRoom == id);
        }

        public async Task CreateConferenceRoomAsync(ConferenceRooms room)
        {
            await _context.ConferenceRooms.AddAsync(room);
            await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteConferenceRoomByIdAsync(int id)
        {
            var deletingRoom = await _context.ConferenceRooms
                .Where(roomId => roomId.IdRoom == id)
                .ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
            return deletingRoom;
        }

        public async Task UpdateConferenceRoomAsync(ConferenceRooms room)
        {
            _context.ConferenceRooms.Update(room);
            await _context.SaveChangesAsync();
        }

        public async Task AddServicesToRoomAsync(int roomId, List<int> serviceId)
        {
            var room = await _context.ConferenceRooms
                .Include(r => r.CompanyServices)
                .FirstOrDefaultAsync(r => r.IdRoom == roomId);

            if (room == null)
                throw new InvalidOperationException("Комната не найдена");

            var services = await _context.CompanyServices
                .Where(cs => serviceId.Contains(cs.IdService))
                .ToListAsync();

            foreach (var service in services)
            {
                if (!room.CompanyServices.Contains(service))
                {
                    room.CompanyServices.Add(service);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<bool> AnyConferenceRoomIdAsync(int id)
        {
            return await _context.ConferenceRooms
                .AnyAsync(roomId => roomId.IdRoom == id);
        }

        public async Task<bool> AnyConferenceRoomNameAsync(string name)
        {
            return await _context.ConferenceRooms
                .AnyAsync(roomName => roomName.NameRoom == name);
        }

        public async Task<IEnumerable<ConferenceRooms>> GetBookedRoomsAsync(DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            return await _context.Bookings
                .Where(b => b.BookingDate.Date == date.Date &&
                            ((b.StartTime < endTime && b.EndTime > startTime)))
                .Select(b => b.ConferenceRooms)
                .ToListAsync();
        }

        public async Task<IEnumerable<ConferenceRooms>> GetAvailableRoomsAsync(DateTime date, TimeSpan startTime, TimeSpan endTime, int capacity)
        {
            var allRooms = await GetAllConferenceRoomsAsync();
            var bookedRooms = await GetBookedRoomsAsync(date, startTime, endTime);

            return allRooms
                .Where(room => room.Capacity >= capacity && !bookedRooms.Any(br => br.IdRoom == room.IdRoom))
                .ToList();
        }

    }
}
