namespace ConferenceRoomsWebAPI.Entity
{
    public class Booking
    {
        public int IdBooking { get; set; }
        public int IdConferenceRoom { get; set; }
        public ConferenceRooms ConferenceRooms { get; set; }
        public DateTime BookingDate { get; set; } 
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public decimal TotalPrice { get; set; }

        public List<BookingCompanyService> BookingCompanyServices { get; set; }
    }
}
