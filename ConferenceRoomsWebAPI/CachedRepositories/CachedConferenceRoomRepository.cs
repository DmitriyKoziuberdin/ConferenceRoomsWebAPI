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

        public async Task<List<ConferenceRooms>> GetAllConferenceRoomsAsync()
        {
            var room = await _memoryCache
               .GetOrCreateAsync(_cacheKey, (entry) => _roomRepository.GetAllConferenceRoomsAsync());
            return room!.ToList();
        }

        public async Task<ConferenceRooms> GetConferenceRoomByIdAsync(int id)
        {
            return await _roomRepository.GetConferenceRoomByIdAsync(id);
        }

        public async Task CreateConferenceRoomAsync(ConferenceRooms room)
        {
            _memoryCache.Remove(_cacheKey);
            await _roomRepository.CreateConferenceRoomAsync(room);
        }

        public async Task UpdateConferenceRoomAsync(ConferenceRooms room)
        {
            _memoryCache.Remove(_cacheKey);
            await _roomRepository.UpdateConferenceRoomAsync(room);
        }

        public async Task<int> DeleteConferenceRoomByIdAsync(int id)
        {
            _memoryCache.Remove(_cacheKey);
            return await _roomRepository.DeleteConferenceRoomByIdAsync(id);

        }

        public async Task AddServicesToRoomAsync(int roomId, List<int> serviceId)
        {
            _memoryCache.Remove(_cacheKey);
            await _roomRepository.AddServicesToRoomAsync(roomId, serviceId);
        }

        public async Task<bool> AnyConferenceRoomIdAsync(int id)
        {
            return await _roomRepository.AnyConferenceRoomIdAsync(id);
        }

        public async Task<bool> AnyConferenceRoomNameAsync(string name)
        {
            return await _roomRepository.AnyConferenceRoomNameAsync(name);
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
