using ConferenceRoomsWebAPI.DTO.Incoming;
using ConferenceRoomsWebAPI.DTO.Outcoming;
using ConferenceRoomsWebAPI.Entity;

namespace ConferenceRoomsWebAPI.Interfaces
{
    public interface IConferenceRoomService
    {
        public Task<List<ConferenceRooms>> GetAllConferenceRooms();
        public Task<ConferenceRoomResponse> GetConferenceRoom(int id);
        public Task CreateConferenceRoom(ConferenceRoomRequest room);
        public Task<ConferenceRoomResponse> UpdateConferenceRoom(int roomId, ConferenceRoomRequest rooms);
        public Task DeleteConfereceRoom(int id);

        //public Task AddOrder(int clientId, int orderId);
        //public Task<ClientDto> GetTotalOrderPrice(int id);
    }
}
