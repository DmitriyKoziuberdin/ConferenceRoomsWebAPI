namespace ConferenceRoomsWebAPI.Entity
{
    public class BookingCompanyService
    {
        public int IdCompanyService { get; set; }
        public CompanyServices CompanyServices { get; set; }

        public int IdBooking { get; set; }
        public Booking Booking { get; set; }
    }
}
