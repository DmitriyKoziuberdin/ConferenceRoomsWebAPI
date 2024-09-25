namespace ConferenceRoomsWebAPI.DTO.Incoming
{
    public class CompanyServiceRequest
    {
        public string ServiceName { get; set; } = null!;
        public decimal PriceService { get; set; }
    }
}
