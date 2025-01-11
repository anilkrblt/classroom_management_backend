using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassroomManagementPresentation.Controllers
{
    [Route("api/rooms")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public RoomsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDto>>> GetRooms()
        {
            var rooms = await _serviceManager.RoomService.GetAllRoomsAsync(trackChanges: false);
            return Ok(rooms);
        }

        // GET: api/Rooms/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDto>> GetRoom(int id)
        {
            var room = await _serviceManager.RoomService.GetRoomByIdAsync(id, trackChanges: false);

            if (room == null)
                return NotFound($"Room with ID {id} not found.");

            return Ok(room);
        }

        // GET: api/Rooms/ByBuilding/{buildingId}
        [HttpGet("building/{buildingId}")]
        public async Task<ActionResult<IEnumerable<RoomDto>>> GetRoomsByBuilding(int buildingId)
        {
            var rooms = await _serviceManager.RoomService.GetRoomsByBuildingIdAsync(buildingId, trackChanges: false);

            if (rooms == null || !rooms.Any())
                return NotFound($"No rooms found for Building with ID {buildingId}.");

            return Ok(rooms);
        }

        // GET: api/Rooms/ByDepartment/{departmentId}
        [HttpGet("department/{departmentId}")]
        public async Task<ActionResult<IEnumerable<RoomDto>>> GetRoomsByDepartment(int departmentId)
        {
            var rooms = await _serviceManager.RoomService.GetRoomsByDepartmentIdAsync(departmentId, trackChanges: false);

            if (rooms == null || !rooms.Any())
                return NotFound($"No rooms found for Department with ID {departmentId}.");

            return Ok(rooms);
        }

        // POST: api/Rooms
        [HttpPost]
        public async Task<IActionResult> CreateRoom([FromBody] RoomCreationForBuildingDto creationDto)
        {
            if (creationDto == null)
                return BadRequest("RoomCreationForBuildingDto is null.");

            // 1) Servis çağrısı
            var createdRoomDto = await _serviceManager.RoomService.CreateRoomAsync(creationDto);

            // 2) 201 Created dönmek istersek => 
            //    route parametresi vs. nameOf(GetRoom) ekleyebilirsiniz:
            return CreatedAtAction(nameof(GetRoom),
                new { id = createdRoomDto.RoomId }, createdRoomDto);
        }


        // PUT: api/Rooms/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRoom(int id, [FromBody] RoomUpdateForBuildingDto updateDto)
        {
            if (updateDto == null)
                return BadRequest("RoomUpdateForBuildingDto is null.");

            await _serviceManager.RoomService.UpdateRoomAsync(id, updateDto);
            return NoContent();
        }

        [HttpPut("close")]
        public async Task<ActionResult> CloseRoom(string roomName)
        {
            await _serviceManager.RoomService.UpdateRoomStatusByNameAsync(roomName);
            return NoContent();
        }

        // DELETE: api/Rooms/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRoom(int id)
        {
            await _serviceManager.RoomService.DeleteRoomAsync(id);
            return NoContent();
        }
    }
}
