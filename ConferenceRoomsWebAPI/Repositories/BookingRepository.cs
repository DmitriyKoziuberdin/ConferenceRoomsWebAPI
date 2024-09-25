using ConferenceRoomsWebAPI.ApplicationDb;
using ConferenceRoomsWebAPI.Entity;
using ConferenceRoomsWebAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomsWebAPI.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<Booking> GetBookingByIdAsync(int id)
        {
            return await _context.Bookings
                .Include(b => b.BookingCompanyServices)
                .ThenInclude(bcs => bcs.CompanyServices)
                .Include(b => b.ConferenceRooms)
                .FirstOrDefaultAsync(b => b.IdBooking == id);
        }

        public async Task<IEnumerable<CompanyServices>> GetServicesByIdsAsync(List<int> serviceIds)
        {
            return await _context.CompanyServices
                .Where(cs => serviceIds.Contains(cs.IdService))
                .ToListAsync();
        }
    }
}
