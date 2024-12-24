using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassroomManagementPresentation.Controllers
{
    [ApiController]
    [Route("api/lecturesessions")]
    public class LectureSessionController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public LectureSessionController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: api/LectureSessions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LectureSessionDto>>> GetLectureSessions()
        {
            var lectureSessions = await _serviceManager.LectureSessionService.GetAllLectureSessionsAsync(trackChanges: false);
            return Ok(lectureSessions);
        }

        // GET: api/LectureSessions/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<LectureSessionDto>> GetLectureSession(int id)
        {
            var lectureSession = await _serviceManager.LectureSessionService.GetLectureSessionByIdAsync(id, trackChanges: false);

            if (lectureSession == null)
                return NotFound($"LectureSession with ID {id} not found.");

            return Ok(lectureSession);
        }

        // GET: api/LectureSessions/ByInstructor/{instructorId}
        [HttpGet("ByInstructor/{instructorId}")]
        public async Task<ActionResult<IEnumerable<LectureSessionDto>>> GetLectureSessionsByInstructor(int instructorId)
        {
            var lectureSessions = await _serviceManager.LectureSessionService.GetLectureSessionsByInstructorIdAsync(instructorId, trackChanges: false);

            if (lectureSessions == null || !lectureSessions.Any())
                return NotFound($"No lecture sessions found for Instructor with ID {instructorId}.");

            return Ok(lectureSessions);
        }

        // POST: api/LectureSessions
        [HttpPost]
        public async Task<ActionResult> CreateLectureSession([FromBody] LectureSessionDto lectureSessionDto)
        {
            if (lectureSessionDto == null)
                return BadRequest("LectureSessionDto object is null.");

            await _serviceManager.LectureSessionService.CreateLectureSessionAsync(lectureSessionDto);
            return CreatedAtAction(nameof(GetLectureSession), new { id = lectureSessionDto.LectureSessionId }, lectureSessionDto);
        }

        // PUT: api/LectureSessions/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateLectureSession(int id, [FromBody] LectureSessionDto lectureSessionDto)
        {
            if (lectureSessionDto == null)
                return BadRequest("LectureSessionDto object is null.");

            await _serviceManager.LectureSessionService.UpdateLectureSessionAsync(id, lectureSessionDto);
            return NoContent();
        }

        // DELETE: api/LectureSessions/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLectureSession(int id)
        {
            await _serviceManager.LectureSessionService.DeleteLectureSessionAsync(id);
            return NoContent();
        }
    }
}
