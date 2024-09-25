using ConferenceRoomsWebAPI.DTO.Incoming;
using ConferenceRoomsWebAPI.DTO.Outcoming;
using ConferenceRoomsWebAPI.Entity;
using ConferenceRoomsWebAPI.Interfaces;

namespace ConferenceRoomsWebAPI.Services
{
    public class BookingSerivce : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IConferenceRoomRepository _conferenceRoomRepository;

        public BookingSerivce(IBookingRepository bookingRepository, IConferenceRoomRepository conferenceRoomRepository)
        {
            _bookingRepository = bookingRepository;
            _conferenceRoomRepository = conferenceRoomRepository;
        }

        public async Task<BookingResponse> CreateBookingAsync(BookingRequest request)
        {
            var bookedRooms = await _conferenceRoomRepository.GetBookedRoomsAsync(
                request.BookingDate, 
                request.StartTime, 
                request.EndTime
            );
            if (bookedRooms.Any(r => r.IdRoom == request.RoomId))
                throw new InvalidOperationException("The room has already been booked for the above period.");

            var availableRooms = await _conferenceRoomRepository.GetAvailableRoomsAsync(
                request.BookingDate,
                request.StartTime,
                request.EndTime,
                request.Capacity
            );

            var room = availableRooms.FirstOrDefault(r => r.IdRoom == request.RoomId);
            if (room == null)
                throw new InvalidOperationException("The room is not available for booking during the hours indicated.");

            var bookingCost = await CalculateBookingCostAsync(room, request.StartTime, request.EndTime, request.CompanyServiceIds);

            var booking = new Booking
            {
                BookingDate = request.BookingDate,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                TotalPrice = bookingCost,
                IdConferenceRoom = room.IdRoom,
                BookingCompanyServices = new List<BookingCompanyService>()
            };

            if (request.CompanyServiceIds != null && request.CompanyServiceIds.Any())
            {
                foreach (var serviceId in request.CompanyServiceIds)
                {
                    booking.BookingCompanyServices.Add(new BookingCompanyService
                    {
                        IdCompanyService = serviceId
                    });
                }
            }

            var createdBooking = await _bookingRepository.CreateBookingAsync(booking);

            foreach (var bookingService in createdBooking.BookingCompanyServices)
            {
                bookingService.IdBooking = createdBooking.IdBooking;
            }

            return new BookingResponse
            {
                IdBooking = createdBooking.IdBooking,
                BookingDate = createdBooking.BookingDate,
                StartTime = createdBooking.StartTime,
                EndTime = createdBooking.EndTime,
                TotalPrice = createdBooking.TotalPrice,
                RoomId = room.IdRoom,
                RoomName = room.NameRoom,
                CompanyServices = createdBooking.BookingCompanyServices.Select(bcs => new CompanyServiceForBookingResponse
                {
                    IdService = bcs.CompanyServices.IdService,
                    ServiceName = bcs.CompanyServices.ServiceName,
                    PriceService = bcs.CompanyServices.PriceService
                }).ToList()
            };
        }

        public async Task<BookingResponse> GetBookingByIdAsync(int id)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(id);
            if (booking == null)
                throw new InvalidOperationException("Бронирование не найдено.");

            return new BookingResponse
            {
                IdBooking = booking.IdBooking,
                BookingDate = booking.BookingDate,
                StartTime = booking.StartTime,
                EndTime = booking.EndTime,
                TotalPrice = booking.TotalPrice,
                RoomId = booking.ConferenceRooms.IdRoom,
                RoomName = booking.ConferenceRooms.NameRoom,
                CompanyServices = booking.BookingCompanyServices.Select(bcs => new CompanyServiceForBookingResponse
                {
                    IdService = bcs.CompanyServices.IdService,
                    ServiceName = bcs.CompanyServices.ServiceName,
                    PriceService = bcs.CompanyServices.PriceService
                }).ToList()
            };
        }

        public async Task<decimal> CalculateBookingCostAsync(ConferenceRooms room, TimeSpan startTime, TimeSpan endTime, List<int> serviceIds)
        {
            var totalHours = (endTime - startTime).TotalHours;
            decimal basePrice = room.BasePricePerHour * (decimal)totalHours;

            if (startTime.Hours >= 9 && endTime.Hours <= 18) //Standard hour
            {
                basePrice *= 1;
            }
            else if (startTime.Hours >= 18 && endTime.Hours <= 23) //Evening time
            {
                basePrice *= 0.8m;
            }
            else if (startTime.Hours >= 6 && endTime.Hours <= 9) //Morning time
            {
                basePrice *= 0.9m;
            }
            else if (startTime.Hours >= 12 && endTime.Hours <= 14) //Peak time
            {
                basePrice *= 1.15m;
            }

            var services = await _bookingRepository.GetServicesByIdsAsync(serviceIds);
            foreach (var service in services)
            {
                basePrice += service.PriceService;
            }

            return basePrice;
        }
    }
}
