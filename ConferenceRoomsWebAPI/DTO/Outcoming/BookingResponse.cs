namespace ConferenceRoomsWebAPI.DTO.Outcoming
{
    public class BookingResponse
    {
        public int IdBooking { get; set; } // Идентификатор бронирования
        public DateTime BookingDate { get; set; } // Дата бронирования
        public TimeSpan StartTime { get; set; } // Время начала бронирования
        public TimeSpan EndTime { get; set; } // Время окончания бронирования
        public decimal TotalPrice { get; set; } // Общая стоимость бронирования
        public int RoomId { get; set; } // Идентификатор комнаты
        public string RoomName { get; set; } // Название комнаты
        public List<CompanyServiceForBookingResponse> CompanyServices { get; set; } // Список услуг, связанных с бронированием
    }
}
