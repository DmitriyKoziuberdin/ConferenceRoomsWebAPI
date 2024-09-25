using ConferenceRoomsWebAPI.Entity;
using ConferenceRoomsWebAPI.Interfaces;
using ConferenceRoomsWebAPI.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace ConferenceRoomsWebAPI.CachedRepositories
{
    public class CachedBookingRepository : IBookingRepository
    {
        private readonly BookingRepository _bookingRepository;
        private readonly IMemoryCache _memoryCache;
        private static string _cacheKey = "booking";

        public CachedBookingRepository(BookingRepository bookingRepository, IMemoryCache memoryCache)
        {
            _bookingRepository = bookingRepository;
            _memoryCache = memoryCache;
        }

        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            _memoryCache.Remove(booking);
            return await _bookingRepository.CreateBookingAsync(booking);
        }

        public async Task<Booking> GetBookingByIdAsync(int id)
        {
            return await _bookingRepository.GetBookingByIdAsync(id);
        }

        public async Task<IEnumerable<CompanyServices>> GetServicesByIdsAsync(List<int> serviceIds)
        {
            return await _bookingRepository.GetServicesByIdsAsync(serviceIds);
        }
    }
}
