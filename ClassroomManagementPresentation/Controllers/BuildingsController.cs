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
        private readonly IBuildingService _buildingService;

        public BuildingsController(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }

        // GET: api/Buildings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BuildingDto>>> GetBuildings()
        {
            var buildings = await _buildingService.GetAllBuildingsAsync(trackChanges: false);
            return Ok(buildings);
        }

        // GET: api/Buildings/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BuildingDto>> GetBuilding(int id)
        {
            var building = await _buildingService.GetBuildingByIdAsync(id, trackChanges: false);
            return Ok(building);
        }

        // GET: api/Buildings/{id}/rooms
        [HttpGet("{id}/rooms")]
        public async Task<ActionResult<IEnumerable<RoomDto>>> GetBuildingRooms(int id)
        {
            var rooms = await _buildingService.GetRoomsByBuildingIdAsync(id, trackChanges: false);
            return Ok(rooms);
        }

        // POST: api/Buildings
        [HttpPost]
        public async Task<ActionResult> CreateBuilding([FromBody] BuildingDto buildingDto)
        {
            if (buildingDto == null)
                return BadRequest("BuildingDto object is null");

            await _buildingService.CreateBuildingAsync(buildingDto);
            return CreatedAtAction(nameof(GetBuilding), new { id = buildingDto.BuildingId }, buildingDto);
        }

        // PUT: api/Buildings/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBuilding(int id, [FromBody] BuildingDto buildingDto)
        {
            if (buildingDto == null)
                return BadRequest("BuildingDto object is null");

            await _buildingService.UpdateBuildingAsync(id, buildingDto);
            return NoContent();
        }

        // DELETE: api/Buildings/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBuilding(int id)
        {
            await _buildingService.DeleteBuildingAsync(id);
            return NoContent();
        }
    }
}
