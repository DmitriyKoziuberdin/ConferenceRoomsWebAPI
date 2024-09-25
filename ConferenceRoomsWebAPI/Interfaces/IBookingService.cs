using ConferenceRoomsWebAPI.DTO.Incoming;
using ConferenceRoomsWebAPI.DTO.Outcoming;

namespace ConferenceRoomsWebAPI.Interfaces
{
    public interface IBookingService
    {
        Task<BookingResponse> CreateBookingAsync(BookingRequest request);
        Task<BookingResponse> GetBookingByIdAsync(int id);
    }
}
