using ConferenceRoomsWebAPI.Entity;

namespace ConferenceRoomsWebAPI.DTO.Incoming
{
    public class ConferenceRoomRequest
    {
        public string NameRoom { get; set; } = null!;
        public int Capacity { get; set; }
        public decimal BasePricePerHour { get; set; }
    }
}
