using ConferenceRoomsWebAPI.DTO.Incoming;
using ConferenceRoomsWebAPI.DTO.Outcoming;
using ConferenceRoomsWebAPI.Entity;
using ConferenceRoomsWebAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomsWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RentController : ControllerBase
    {
        private readonly IConferenceRoomService _conferenceRoom;

        public RentController(IConferenceRoomService conferenceRoom)
        {
            _conferenceRoom = conferenceRoom;
        }

        [HttpGet]
        public async Task<List<ConferenceRooms>> GetAllConfereceRooms()
        {
            return await _conferenceRoom.GetAllConferenceRooms();
        }

        [HttpGet("{id:int}")]
        public async Task<ConferenceRoomResponse> GetConfereceRoom([FromRoute] int id)
        {
            return await _conferenceRoom.GetConferenceRoom(id);
        }

        [HttpPost]
        public async Task<IActionResult> CreateConferenceRoom([FromBody] ConferenceRoomRequest room)
        {
            await _conferenceRoom.CreateConferenceRoom(room);
            return Ok();
        }

        [HttpPut("{roomId:int}/")]
        public async Task<ActionResult<ConferenceRoomResponse>> UpdateConferenceRoom([FromRoute] int roomId, [FromBody] ConferenceRoomRequest room)
        {
            return new OkObjectResult(await _conferenceRoom.UpdateConferenceRoom(roomId, room));
        }

        [HttpDelete("{roomId:int}")]
        public async Task<IActionResult> DeleteConferenceRoom([FromRoute] int roomId)
        {
            await _conferenceRoom.DeleteConfereceRoom(roomId);
            return Ok();
        }

        //[HttpPost("{clientId:int}/order/{orderId:int}")]
        //public async Task<IActionResult> CreateClient([FromRoute] int clientId, [FromRoute] int orderId)
        //{
        //    await _clientService.AddOrder(clientId, orderId);
        //    return Ok();
        //}

        //[HttpGet("{clientId:int}/totalOrderPrice")]
        //public async Task<ClientDto> GetTotalOrderPrice(int clientId)
        //{
        //    return await _clientService.GetTotalOrderPrice(clientId);
        //}
    }
}
