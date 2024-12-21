using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace backend.controllers
{
    [Route("api/rooms")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public RoomsController(IServiceManager servisManager)
        {
            _serviceManager = servisManager;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllRooms()
        {

            var rooms = await _serviceManager.RoomService.GetAllRoomsAsync(false);
            return Ok(rooms);
        }

        [HttpGet("/classes")]
        public async Task<IActionResult> GetAllClasses()
        {
            var classes = await _serviceManager.RoomService.GetAllRoomsAsync(false);
            return Ok(classes);
        }


        [HttpGet("/halls")]
        public async Task<IActionResult> GetAllLectureHalls()
        {
            var rooms = await _serviceManager.RoomService.GetAllLectureHallsAsync(false);
            return Ok(rooms);
        }

        [HttpGet("/labs")]
        public async Task<IActionResult> GetAllLabs()
        {
            var labs = await _serviceManager.RoomService.GetAllLabsAsync(false);
            return Ok(labs);
        }
    }
}