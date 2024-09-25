using ConferenceRoomsWebAPI.Entity;

namespace ConferenceRoomsWebAPI.Interfaces
{
    public interface IBookingRepository
    {
        public Task<Booking> CreateBookingAsync(Booking booking);
        public Task<Booking> GetBookingByIdAsync(int id);
        public Task<IEnumerable<CompanyServices>> GetServicesByIdsAsync(List<int> serviceIds);
    }
}
