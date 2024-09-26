using ConferenceRoomsWebAPI.DTO.Incoming;
using ConferenceRoomsWebAPI.DTO.Outcoming;
using ConferenceRoomsWebAPI.Entity;

namespace ConferenceRoomsWebAPI.Interfaces
{
    public interface IConferenceRoomService
    {
        public Task<List<ConferenceRooms>> GetAllConferenceRoomsAsync();
        public Task<ConferenceRoomResponse> GetConferenceRoomByIdAsync(int id);
        public Task CreateConferenceRoomAsync(ConferenceRoomRequest room);
        public Task<ConferenceRoomResponse> UpdateConferenceRoomAsync(int roomId, ConferenceRoomRequest rooms);
        public Task DeleteConfereceRoomByIdAsync(int id);
        public Task AddServicesToRoomAsync(int roomId, List<int> serviceIds);
        public Task<IEnumerable<ConferenceRoomResponse>> GetAvailableRoomsAsync(DateTime date, TimeSpan startTime, TimeSpan endTime, int capacity);
    }
}
