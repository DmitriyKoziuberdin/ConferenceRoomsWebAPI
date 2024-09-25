using ConferenceRoomsWebAPI.Entity;
using ConferenceRoomsWebAPI.Interfaces;
using ConferenceRoomsWebAPI.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace ConferenceRoomsWebAPI.CachedRepositories
{
    public class CachedConferenceRoomRepository : IConferenceRoomRepository
    {
        private readonly ConferenceRoomRepository _roomRepository;
        private readonly IMemoryCache _memoryCache;
        private static string _cacheKey = "room";

        public CachedConferenceRoomRepository(ConferenceRoomRepository roomRepository, IMemoryCache memoryCache)
        {
            _roomRepository = roomRepository;
            _memoryCache = memoryCache;
        }

        public async Task<List<ConferenceRooms>> GetAllConferenceRooms()
        {
            var room = await _memoryCache
               .GetOrCreateAsync(_cacheKey, (entry) => _roomRepository.GetAllConferenceRooms());
            return room!.ToList();
        }

        public async Task<ConferenceRooms> GetConferenceRoomId(int id)
        {
            return await _roomRepository.GetConferenceRoomId(id);
        }

        public async Task CreateConferenceRoom(ConferenceRooms room)
        {
            _memoryCache.Remove(_cacheKey);
            await _roomRepository.CreateConferenceRoom(room);
        }

        public async Task UpdateConferenceRoom(ConferenceRooms room)
        {
            _memoryCache.Remove(_cacheKey);
            await _roomRepository.UpdateConferenceRoom(room);
        }

        public async Task<int> DeleteConferenceRoomById(int id)
        {
            _memoryCache.Remove(_cacheKey);
            return await _roomRepository.DeleteConferenceRoomById(id);

        }

        public async Task AddServicesToRoom(int roomId, List<int> serviceId)
        {
            _memoryCache.Remove(_cacheKey);
            await _roomRepository.AddServicesToRoom(roomId, serviceId);
        }

        public async Task<bool> AnyConferenceRoomId(int id)
        {
            return await _roomRepository.AnyConferenceRoomId(id);
        }

        public async Task<bool> AnyConferenceRoomName(string name)
        {
            return await _roomRepository.AnyConferenceRoomName(name);
        }

        public async Task<IEnumerable<ConferenceRooms>> GetAvailableRoomsAsync(DateTime date, TimeSpan startTime, TimeSpan endTime, int capacity)
        {
            return await _roomRepository.GetAvailableRoomsAsync(date, startTime, endTime, capacity);
        }

        public async Task<IEnumerable<ConferenceRooms>> GetBookedRoomsAsync(DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            return await _roomRepository.GetBookedRoomsAsync(date, startTime, endTime);
        }
    }
}
