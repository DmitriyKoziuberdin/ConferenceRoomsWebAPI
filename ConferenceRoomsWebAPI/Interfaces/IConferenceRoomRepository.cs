using ConferenceRoomsWebAPI.Entity;

namespace ConferenceRoomsWebAPI.Interfaces
{
    public interface IConferenceRoomRepository
    {
        public Task<List<ConferenceRooms>> GetAllConferenceRooms();
        public Task<ConferenceRooms> GetConferenceRoom(int id);
        public Task CreateConferenceRoom(ConferenceRooms room);
        public Task UpdateConferenceRoom(ConferenceRooms room);
        public Task<int> DeleteConferenceRoomById(int id);
        public Task<bool> AnyConferenceRoomId(int id);
        public Task<bool> AnyConferenceRoomName(string name);

        //public Task AddOrder(int clientId, int orderId);
    }
}
