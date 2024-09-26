using ConferenceRoomsWebAPI.DTO.Incoming;
using ConferenceRoomsWebAPI.DTO.Outcoming;
using ConferenceRoomsWebAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomsWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _booking;

        public BookingController(IBookingService booking)
        {
            _booking = booking;
        }

        [HttpPost("createBooking")]
        public async Task<ActionResult<BookingResponse>> CreateBooking([FromBody] BookingRequest request)
        {
            if (request == null)
                return BadRequest("Request cannot be null.");

            // Перевірка валідності вхідних даних
            if (request.RoomId <= 0 || request.Capacity <= 0 ||
                request.StartTime >= request.EndTime ||
                request.CompanyServiceIds == null || request.CompanyServiceIds.Count == 0)
            {
                return BadRequest("Invalid booking details.");
            }
            try
            {
                var response = await _booking.CreateBookingAsync(request);
                return CreatedAtAction(nameof(CreateBooking), response);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("{bookingId:int}/")]
        public async Task<ActionResult<BookingResponse>> GetBookingById([FromRoute]int bookingId)
        {
            var response = await _booking.GetBookingByIdAsync(bookingId);
            return Ok(response);
        }
    }
}

