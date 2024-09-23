namespace ConferenceRoomsWebAPI.Entity
{
    public class ConferenceRooms
    {
        public int IdRoom { get; set; }
        public string NameRoom { get; set; } = null!;
        public int Capacity { get; set; }
        public decimal BasePricePerHour { get; set; }
        public List<CompanyServices> CompanyServices { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}
