using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassroomManagementPresentation.Controllers
{
    [ApiController]
    [Route("api/clubs")]
    public class ClubsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public ClubsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: api/Clubs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClubDto>>> GetClubs()
        {
            var clubs = await _serviceManager.ClubService.GetAllClubsAsync(trackChanges: false);
            return Ok(clubs);
        }

        // GET: api/Clubs/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ClubDto>> GetClub(int id)
        {
            var club = await _serviceManager.ClubService.GetClubByIdAsync(id, trackChanges: false);

            if (club == null)
                return NotFound($"Club with ID {id} not found.");

            return Ok(club);
        }

        // GET: api/Clubs/{id}/Clubmembers
        [HttpGet("{id}/Clubmembers")]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetClubMembers(int id)
        {
            var members = await _serviceManager.ClubService.GetClubMembersAsync(id, trackChanges: false);

            if (members == null || !members.Any())
                return NotFound($"No members found for Club with ID {id}.");

            return Ok(members);
        }

        // PUT: api/Clubs/{id}/update
        [HttpPut("{id}/update")]
        public async Task<ActionResult> UpdateClubManager([FromRoute] int id, [FromBody] int studentId)
        {
            await _serviceManager.ClubService.UpdateClubManagerAsync(id, studentId);
            return NoContent();
        }

        // POST: api/Clubs
        [HttpPost]
        public async Task<ActionResult> CreateClub([FromBody] ClubDto clubDto)
        {
            if (clubDto == null)
                return BadRequest("ClubDto object is null.");

            await _serviceManager.ClubService.CreateClubAsync(clubDto);
            return CreatedAtAction(nameof(GetClub), new { id = clubDto.ClubId }, clubDto);
        }

        // PUT: api/Clubs/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateClub(int id, [FromBody] ClubDto clubDto)
        {
            if (clubDto == null)
                return BadRequest("ClubDto object is null.");

            await _serviceManager.ClubService.UpdateClubAsync(id, clubDto);
            return NoContent();
        }

        // DELETE: api/Clubs/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClub(int id)
        {
            await _serviceManager.ClubService.DeleteClubAsync(id);
            return NoContent();
        }
    }
}
