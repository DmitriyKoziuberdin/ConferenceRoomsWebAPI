namespace ConferenceRoomsWebAPI.DTO.Incoming
{
    public class BookingRequest
    {
        public int RoomId { get; set; } // Идентификатор комнаты, которую нужно забронировать
        public DateTime BookingDate { get; set; } // Дата бронирования
        public TimeSpan StartTime { get; set; } // Время начала бронирования
        public TimeSpan EndTime { get; set; } // Время окончания бронирования
        public int Capacity { get; set; } // Требуемая вместимость комнаты
        public List<int> CompanyServiceIds { get; set; } // Список идентификаторов услуг, которые нужно добавить
    }
}
