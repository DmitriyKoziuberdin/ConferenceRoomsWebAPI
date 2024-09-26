using Common.Exceptions;
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

        public async Task<List<ConferenceRooms>> GetAllConferenceRoomsAsync()
        {
            return await _conferenceRoomRepository.GetAllConferenceRoomsAsync();
        }

        public async Task<ConferenceRoomResponse> GetConferenceRoomByIdAsync(int id)
        {
            var isExist = await _conferenceRoomRepository.AnyConferenceRoomIdAsync(id);
            if (!isExist)
                throw new ConferenceRoomNotFoundException($"Conference room with this ID: {id} not found.");

            var roomId = await _conferenceRoomRepository.GetConferenceRoomByIdAsync(id);
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

        public async Task CreateConferenceRoomAsync(ConferenceRoomRequest room)
        {
            var isExist = await _conferenceRoomRepository.AnyConferenceRoomNameAsync(room.NameRoom);
            if (isExist)
                throw new ConferenceRoomDuplicateNameException($"Conference room with this {room.NameRoom} already use.");

            await _conferenceRoomRepository.CreateConferenceRoomAsync(new ConferenceRooms
            {
                NameRoom = room.NameRoom,
                Capacity = room.Capacity,
                BasePricePerHour = room.BasePricePerHour
            });
        }

        public async Task DeleteConfereceRoomByIdAsync(int id)
        {
            var isExist = await _conferenceRoomRepository.AnyConferenceRoomIdAsync(id);
            if (!isExist)
                throw new ConferenceRoomNotFoundException($"Conference room with this ID: {id} not found.");

            await _conferenceRoomRepository.DeleteConferenceRoomByIdAsync(id);
        }

        public async Task<ConferenceRoomResponse> UpdateConferenceRoomAsync(int roomId, ConferenceRoomRequest room)
        {
            var isExistId = await _conferenceRoomRepository.AnyConferenceRoomIdAsync(roomId);
            if (!isExistId)
                throw new ConferenceRoomNotFoundException($"Conference room with this ID: {roomId} not found.");

            var isExistName = await _conferenceRoomRepository.AnyConferenceRoomNameAsync(room.NameRoom);
            if (isExistName)
                throw new ConferenceRoomDuplicateNameException($"Conference room with this {room.NameRoom} already use.");

            var newRoom = new ConferenceRooms
            {
                IdRoom = roomId,
                NameRoom = room.NameRoom,
                Capacity = room.Capacity,
                BasePricePerHour = room.BasePricePerHour,
            };

            await _conferenceRoomRepository.UpdateConferenceRoomAsync(newRoom);
            var updatingRoom = await _conferenceRoomRepository.GetConferenceRoomByIdAsync(newRoom.IdRoom);
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
            var isRoomExist = await _conferenceRoomRepository.AnyConferenceRoomIdAsync(roomId);
            if (!isRoomExist)
                throw new ConferenceRoomNotFoundException($"Conference room with this ID: {roomId} not found.");

            await _conferenceRoomRepository.AddServicesToRoomAsync(roomId, serviceIds);
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
