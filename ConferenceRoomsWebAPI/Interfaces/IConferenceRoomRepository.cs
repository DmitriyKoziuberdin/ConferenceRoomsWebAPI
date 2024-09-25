using ConferenceRoomsWebAPI.Entity;

namespace ConferenceRoomsWebAPI.Interfaces
{
    public interface IConferenceRoomRepository
    {
        public Task<List<ConferenceRooms>> GetAllConferenceRoomsAsync();
        public Task<ConferenceRooms> GetConferenceRoomByIdAsync(int id);
        public Task CreateConferenceRoomAsync(ConferenceRooms room);
        public Task UpdateConferenceRoomAsync(ConferenceRooms room);
        public Task<int> DeleteConferenceRoomByIdAsync(int id);
        public Task AddServicesToRoomAsync(int roomId, List<int> serviceId);
        public Task<bool> AnyConferenceRoomIdAsync(int id);
        public Task<bool> AnyConferenceRoomNameAsync(string name);
        public Task<IEnumerable<ConferenceRooms>> GetAvailableRoomsAsync(DateTime date, TimeSpan startTime, TimeSpan endTime, int capacity);
        public Task<IEnumerable<ConferenceRooms>> GetBookedRoomsAsync(DateTime date, TimeSpan startTime, TimeSpan endTime);
    }
}
    