using ConferenceRoomsWebAPI.Entity;

namespace ConferenceRoomsWebAPI.Interfaces
{
    public interface IConferenceRoomRepository
    {
        public Task<List<ConferenceRooms>> GetAllConferenceRooms();
        public Task<ConferenceRooms> GetConferenceRoomId(int id);
        public Task CreateConferenceRoom(ConferenceRooms room);
        public Task UpdateConferenceRoom(ConferenceRooms room);
        public Task<int> DeleteConferenceRoomById(int id);
        public Task AddServicesToRoom(int roomId, List<int> serviceId);
        public Task<bool> AnyConferenceRoomId(int id);
        public Task<bool> AnyConferenceRoomName(string name);
        public Task<IEnumerable<ConferenceRooms>> GetAvailableRoomsAsync(DateTime date, TimeSpan startTime, TimeSpan endTime, int capacity);
        public Task<IEnumerable<ConferenceRooms>> GetBookedRoomsAsync(DateTime date, TimeSpan startTime, TimeSpan endTime);
    }
}
