using ConferenceRoomsWebAPI.Entity;

namespace ConferenceRoomsWebAPI.DTO.Outcoming
{
    public class ConferenceRoomResponse
    {
        public int IdRoom { get; set; }
        public string NameRoom { get; set; } = null!;
        public int Capacity { get; set; }
        public decimal BasePricePerHour { get; set; }
        public List<CompanyServiceForConferenceRoomResponse>? CompanyServices { get; set; }
    }
}
