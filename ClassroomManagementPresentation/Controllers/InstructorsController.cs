using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassroomManagementPresentation.Controllers
{
    [Route("api/instructors")]
    [ApiController]
    public class InstructorsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public InstructorsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: api/Instructors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstructorDto>>> GetInstructors()
        {
            var instructors = await _serviceManager.InstructorService.GetAllInstructorsAsync(trackChanges: false);
            return Ok(instructors);
        }

        // GET: api/Instructors/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<InstructorDto>> GetInstructor(int id)
        {
            var instructor = await _serviceManager.InstructorService.GetInstructorByIdAsync(id, trackChanges: false);

            if (instructor == null)
                return NotFound($"Instructor with ID {id} not found.");

            return Ok(instructor);
        }

        // GET: api/Instructors/{id}/lectures
        [HttpGet("{id}/lectures")]
        public async Task<ActionResult<IEnumerable<LectureDto>>> GetInstructorsLectures(int id)
        {
            var lectures = await _serviceManager.InstructorService.GetInstructorLecturesAsync(id, trackChanges: false);

            if (lectures == null || !lectures.Any())
                return NotFound($"No lectures found for Instructor with ID {id}.");

            return Ok(lectures);
        }

        // POST: api/Instructors
        [HttpPost]
        public async Task<ActionResult> CreateInstructor([FromBody] InstructorDto instructorDto)
        {
            if (instructorDto == null)
                return BadRequest("InstructorDto object is null.");

            await _serviceManager.InstructorService.CreateInstructorAsync(instructorDto);
            return CreatedAtAction(nameof(GetInstructor), new { id = instructorDto.InstructorId }, instructorDto);
        }

        // PUT: api/Instructors/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateInstructor(int id, [FromBody] InstructorDto instructorDto)
        {
            if (instructorDto == null)
                return BadRequest("InstructorDto object is null.");

            await _serviceManager.InstructorService.UpdateInstructorAsync(id, instructorDto);
            return NoContent();
        }

        // DELETE: api/Instructors/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteInstructor(int id)
        {
            await _serviceManager.InstructorService.DeleteInstructorAsync(id);
            return NoContent();
        }
    }
}
