﻿namespace ConferenceRoomsWebAPI.Entity
{
    public class CompanyServices
    {
        public int IdService { get; set; }
        public string ServiceName { get; set; } = null!;
        public decimal PriceService { get; set; }
        public List<BookingCompanyService> BookingCompanyServices { get; set; }
    }
}
