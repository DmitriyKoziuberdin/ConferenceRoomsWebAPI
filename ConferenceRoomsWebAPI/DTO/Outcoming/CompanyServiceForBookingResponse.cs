namespace ConferenceRoomsWebAPI.DTO.Outcoming
{
    public class CompanyServiceForBookingResponse
    {
        public int IdService { get; set; } // Идентификатор услуги
        public string ServiceName { get; set; } // Название услуги
        public decimal PriceService { get; set; } // Стоимость услуги
    }
}
