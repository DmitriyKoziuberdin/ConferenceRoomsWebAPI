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

        //public async Task<ConferenceRooms> GetConferenceRoomId(int id)
        //{
        //    return await _context.ConferenceRooms
        //        .Include(cs => cs.CompanyServices)
        //        .FirstAsync(roomId => roomId.IdRoom == id);
        //}

        public async Task<ConferenceRooms> GetConferenceRoom(int id)
        {
            return await _context.ConferenceRooms
                .Include(cs => cs.CompanyServices)
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

        public async Task AddServicesToRoom(int roomId, List<int> serviceId)
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

        //public async Task<IEnumerable<ConferenceRooms>> GetAvailableRoomsAsync(DateTime date, TimeSpan startTime, TimeSpan endTime, int capacity)
        //{
        //    return await _context.ConferenceRooms
        //        .Where(r => r.Capacity >= capacity && !r.Bookings
        //            .Any(b => b.BookingDate == date && b.StartTime < endTime && b.EndTime > startTime))
        //        .ToListAsync();
        //}

        public async Task<IEnumerable<ConferenceRooms>> GetBookedRoomsAsync(DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            return await _context.Bookings
                .Where(b => b.BookingDate.Date == date.Date &&
                            ((b.StartTime < endTime && b.EndTime > startTime)))
                .Select(b => b.ConferenceRooms) // Предполагается, что у вас есть навигационное свойство для конференц-зала
                .ToListAsync();
        }

        public async Task<IEnumerable<ConferenceRooms>> GetAvailableRoomsAsync(DateTime date, TimeSpan startTime, TimeSpan endTime, int capacity)
        {
            var allRooms = await GetAllConferenceRooms();
            var bookedRooms = await GetBookedRoomsAsync(date, startTime, endTime);

            return allRooms
                .Where(room => room.Capacity >= capacity && !bookedRooms.Any(br => br.IdRoom == room.IdRoom))
                .ToList();
        }

    }
}
