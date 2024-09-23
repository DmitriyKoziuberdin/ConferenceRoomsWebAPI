using ConferenceRoomsWebAPI.Entity;

namespace ConferenceRoomsWebAPI.Interfaces.IService
{
    public interface IConferenceRoomservice
    {
        public Task<List<ConferenceRooms>> GetAllConferenceRooms();
    }
}
