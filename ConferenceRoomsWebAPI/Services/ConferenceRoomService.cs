using ConferenceRoomsWebAPI.DTO.Incoming;
using ConferenceRoomsWebAPI.DTO.Outcoming;
using ConferenceRoomsWebAPI.Entity;
using ConferenceRoomsWebAPI.Interfaces;

namespace ConferenceRoomsWebAPI.Services
{
    public class ConferenceRoomService : IConferenceRoomService
    {
        private readonly IConferenceRoomRepository _conferenceRoomRepository;

        public ConferenceRoomService(IConferenceRoomRepository conferenceRoomRepository)
        {
            _conferenceRoomRepository = conferenceRoomRepository;
        }

        public async Task<List<ConferenceRooms>> GetAllConferenceRooms()
        {
            return await _conferenceRoomRepository.GetAllConferenceRooms();
        }

        public async Task<ConferenceRoomResponse> GetConferenceRoom(int id)
        {
            var isExist = await _conferenceRoomRepository.AnyConferenceRoomId(id);
            if (!isExist)
                throw new InvalidOperationException();

            var roomId = await _conferenceRoomRepository.GetConferenceRoom(id);
            var roomResponse = new ConferenceRoomResponse
            {
                IdRoom = roomId.IdRoom,
                NameRoom = roomId.NameRoom,
                Capacity = roomId.Capacity,
                BasePricePerHour = roomId.BasePricePerHour,
                CompanyServices = roomId.CompanyServices.Select(cs =>
                {
                    return new CompanyServiceForConferenceRoomResponse
                    {
                        IdService = cs.IdService,
                        ServiceName = cs.ServiceName,
                        PriceService = cs.PriceService,
                    };
                }).ToList(),
            };

            return roomResponse;
        }

        public async Task CreateConferenceRoom(ConferenceRoomRequest room)
        {
            var isExist = await _conferenceRoomRepository.AnyConferenceRoomName(room.NameRoom);
            if (isExist)
                throw new InvalidOperationException();

            await _conferenceRoomRepository.CreateConferenceRoom(new ConferenceRooms
            {
                NameRoom = room.NameRoom,
                Capacity = room.Capacity,
                BasePricePerHour = room.BasePricePerHour
            });
        }

        public async Task DeleteConfereceRoom(int id)
        {
            var isExist = await _conferenceRoomRepository.AnyConferenceRoomId(id);
            if (!isExist)
                throw new InvalidOperationException();

            await _conferenceRoomRepository.DeleteConferenceRoomById(id);
        }

        public async Task<ConferenceRoomResponse> UpdateConferenceRoom(int roomId, ConferenceRoomRequest room)
        {
            var isExistId = await _conferenceRoomRepository.AnyConferenceRoomId(roomId);
            if (!isExistId)
                throw new InvalidOperationException();

            var isExistName = await _conferenceRoomRepository.AnyConferenceRoomName(room.NameRoom);
            if (isExistName)
                throw new InvalidOperationException();

            var newRoom = new ConferenceRooms
            {
                IdRoom = roomId,
                NameRoom = room.NameRoom,
                Capacity = room.Capacity,
                BasePricePerHour = room.BasePricePerHour,
            };

            await _conferenceRoomRepository.UpdateConferenceRoom(newRoom);
            var updatingRoom = await _conferenceRoomRepository.GetConferenceRoom(newRoom.IdRoom);
            return new ConferenceRoomResponse
            {
                IdRoom = updatingRoom.IdRoom,
                NameRoom = updatingRoom.NameRoom,
                Capacity = updatingRoom.Capacity,
                BasePricePerHour = updatingRoom.BasePricePerHour
            };
        }

        public async Task AddServicesToRoomAsync(int roomId, List<int> serviceIds)
        {
            var isRoomExist = await _conferenceRoomRepository.AnyConferenceRoomId(roomId);
            if (!isRoomExist)
                throw new InvalidOperationException("Room not found");

            await _conferenceRoomRepository.AddServicesToRoom(roomId, serviceIds);
        }

        public async Task<IEnumerable<ConferenceRoomResponse>> GetAvailableRoomsAsync(DateTime date, TimeSpan startTime, TimeSpan endTime, int capacity)
        {
            var availableRooms = await _conferenceRoomRepository.GetAvailableRoomsAsync(date, startTime, endTime, capacity);

            return availableRooms.Select(room => new ConferenceRoomResponse
            {
                IdRoom = room.IdRoom,
                NameRoom = room.NameRoom,
                Capacity = room.Capacity,
                BasePricePerHour = room.BasePricePerHour,
            }).ToList();
        }
    }
}
