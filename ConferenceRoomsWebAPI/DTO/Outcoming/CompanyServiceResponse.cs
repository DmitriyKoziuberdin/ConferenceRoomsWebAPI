namespace ConferenceRoomsWebAPI.DTO.Outcoming
{
    public class CompanyServiceResponse
    {
        public int IdService { get; set; }
        public string ServiceName { get; set; } = null!;
        public decimal PriceService { get; set; }
    }
}
