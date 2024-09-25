using ConferenceRoomsWebAPI.DTO.Incoming;
using ConferenceRoomsWebAPI.DTO.Outcoming;
using ConferenceRoomsWebAPI.Entity;
using ConferenceRoomsWebAPI.Interfaces;
using ConferenceRoomsWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomsWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConferenceRoomController : ControllerBase
    {
        private readonly IConferenceRoomService _conferenceRoom;

        public ConferenceRoomController(IConferenceRoomService conferenceRoom)
        {
            _conferenceRoom = conferenceRoom;
        }

        [HttpGet]
        public async Task<List<ConferenceRooms>> GetAllConfereceRooms()
        {
            return await _conferenceRoom.GetAllConferenceRoomsAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ConferenceRoomResponse> GetConfereceRoom([FromRoute] int id)
        {
            return await _conferenceRoom.GetConferenceRoomByIdAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> CreateConferenceRoom([FromBody] ConferenceRoomRequest room)
        {
            await _conferenceRoom.CreateConferenceRoomAsync(room);
            return Ok();
        }

        [HttpPut("{roomId:int}/")]
        public async Task<ActionResult<ConferenceRoomResponse>> UpdateConferenceRoom([FromRoute] int roomId, [FromBody] ConferenceRoomRequest room)
        {
            return new OkObjectResult(await _conferenceRoom.UpdateConferenceRoomAsync(roomId, room));
        }

        [HttpDelete("{roomId:int}")]
        public async Task<IActionResult> DeleteConferenceRoom([FromRoute] int roomId)
        {
            await _conferenceRoom.DeleteConfereceRoomByIdAsync(roomId);
            return Ok();
        }

        [HttpPost("{roomId}/services")]
        public async Task<IActionResult> AddServicesToRoom(int roomId, [FromBody] List<int> serviceId)
        {
            if (serviceId == null || serviceId.Count == 0)
                return BadRequest("The list of services cannot be empty");
            try
            {
                await _conferenceRoom.AddServicesToRoomAsync(roomId, serviceId);
                return Ok("Services have been successfully added to the room");
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("availableRoom")]
        public async Task<IEnumerable<ConferenceRoomResponse>> GetAvailableRoomsAsync(DateTime date, TimeSpan startTime, TimeSpan endTime, int capacity)
        {
            return await _conferenceRoom.GetAvailableRoomsAsync(date, startTime, endTime, capacity);
        }
    }
}
