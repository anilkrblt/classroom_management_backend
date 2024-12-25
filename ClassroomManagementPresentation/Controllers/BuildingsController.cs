using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects;
using Service.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassroomManagementPresentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuildingsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public BuildingsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: api/Buildings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BuildingDto>>> GetBuildings()
        {
            var buildings = await _serviceManager.BuildingService.GetAllBuildingsAsync(trackChanges: false);
            return Ok(buildings);
        }

        // GET: api/Buildings/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BuildingDto>> GetBuilding(int id)
        {
            var building = await _serviceManager.BuildingService.GetBuildingByIdAsync(id, trackChanges: false);
            return Ok(building);
        }

        // GET: api/Buildings/{id}/rooms
        [HttpGet("{id}/rooms")]
        public async Task<ActionResult<IEnumerable<RoomDto>>> GetBuildingRooms(int id)
        {
            var rooms = await _serviceManager.BuildingService.GetRoomsByBuildingIdAsync(id, trackChanges: false);
            return Ok(rooms);
        }

        // POST: api/Buildings
        [HttpPost]
        public async Task<ActionResult> CreateBuilding([FromBody] BuildingForCreateDto buildingForCreateDto)
        {
            if (buildingForCreateDto is null)
                return BadRequest("BuildingDto object is null");

            await _serviceManager.BuildingService.CreateBuildingAsync(buildingForCreateDto);
            return Created();
        }

        // PUT: api/Buildings/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBuilding(int id, [FromBody] BuildingForUpdateDto buildingForUpdateDto)
        {
            if (buildingForUpdateDto is null)
                return BadRequest("BuildingDto object is null");

            await _serviceManager.BuildingService.UpdateBuildingAsync(id, buildingForUpdateDto);
            return NoContent();
        }

        // DELETE: api/Buildings/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBuilding(int id)
        {
            await _serviceManager.BuildingService.DeleteBuildingAsync(id);
            return NoContent();
        }
    }
}
