using ConferenceRoomsWebAPI.ApplicationDb;
using ConferenceRoomsWebAPI.DTO.Incoming;
using ConferenceRoomsWebAPI.DTO.Outcoming;
using ConferenceRoomsWebAPI.Entity;
using ConferenceRoomsWebAPI.Interfaces;

namespace ConferenceRoomsWebAPI.Services
{
    public class ConferenceRoomService : IConferenceRoomService
    {
        private readonly IConferenceRoomRepository _conferenceRoom;

        public ConferenceRoomService(IConferenceRoomRepository conferenceRoom)
        {
            _conferenceRoom = conferenceRoom;
        }

        public async Task<List<ConferenceRooms>> GetAllConferenceRooms()
        {
            return await _conferenceRoom.GetAllConferenceRooms();
        }

        public async Task<ConferenceRoomResponse> GetConferenceRoom(int id)
        {
            var isExist = await _conferenceRoom.AnyConferenceRoomId(id);
            if (!isExist)
                throw new InvalidOperationException();

            var roomId = await _conferenceRoom.GetConferenceRoom(id);
            var roomResponse = new ConferenceRoomResponse
            {
                IdRoom = roomId.IdRoom,
                NameRoom = roomId.NameRoom,
                Capacity = roomId.Capacity,
                BasePricePerHour = roomId.BasePricePerHour
            };
            
            return roomResponse;
        }

        public async Task CreateConferenceRoom(ConferenceRoomRequest room)
        {
            var isExist = await _conferenceRoom.AnyConferenceRoomName(room.NameRoom);
            if (isExist)
                throw new InvalidOperationException();

            await _conferenceRoom.CreateConferenceRoom(new ConferenceRooms
            {
                NameRoom = room.NameRoom,
                Capacity = room.Capacity,
                BasePricePerHour= room.BasePricePerHour
            });
        }

        public async Task DeleteConfereceRoom(int id)
        {
            var isExist = await _conferenceRoom.AnyConferenceRoomId(id);
            if (!isExist)
                throw new InvalidOperationException();

            await _conferenceRoom.DeleteConferenceRoomById(id);
        }

        public async Task<ConferenceRoomResponse> UpdateConferenceRoom(int roomId, ConferenceRoomRequest room)
        {
            var isExistId = await _conferenceRoom.AnyConferenceRoomId(roomId);
            if (!isExistId)
                throw new InvalidOperationException();

            var isExistName = await _conferenceRoom.AnyConferenceRoomName(room.NameRoom);
            if (isExistName)
                throw new InvalidOperationException();

            var newRoom = new ConferenceRooms 
            {
                IdRoom = roomId,
                NameRoom = room.NameRoom,
                Capacity = room.Capacity,
                BasePricePerHour = room.BasePricePerHour,
            };

            await _conferenceRoom.UpdateConferenceRoom(newRoom);
            var updatingRoom = await _conferenceRoom.GetConferenceRoom(newRoom.IdRoom);
            return new ConferenceRoomResponse
            {
                IdRoom = updatingRoom.IdRoom,
                NameRoom = updatingRoom.NameRoom,
                Capacity = updatingRoom.Capacity,
                BasePricePerHour= updatingRoom.BasePricePerHour
            };
        }
    }
}
