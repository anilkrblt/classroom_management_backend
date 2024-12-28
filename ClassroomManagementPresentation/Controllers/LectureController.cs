using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassroomManagementPresentation.Controllers
{
    [ApiController]
    [Route("api/lectures")]
    public class LectureController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public LectureController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: api/Lectures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LectureDto>>> GetLectures()
        {
            var lectures = await _serviceManager.LectureService.GetAllLecturesAsync(trackChanges: false);
            return Ok(lectures);
        }

        // GET: api/Lectures/{code}
        [HttpGet("{code}")]
        public async Task<ActionResult<LectureDto>> GetLecture(string code)
        {
            var lecture = await _serviceManager.LectureService.GetLectureByCodeAsync(code, trackChanges: false);

            if (lecture == null)
                return NotFound($"Lecture with code {code} not found.");

            return Ok(lecture);
        }

        // POST: api/Lectures
        [HttpPost]
        public IActionResult CreateLecture([FromBody] LectureCreateDto lectureCreateDto)
        {
            var createdLecture = _serviceManager.LectureService.CreateLectureAsync(lectureCreateDto);
            // HTTP 201 Created + body
            return Ok(createdLecture);
        }



        // PUT: api/Lectures/{code}
        [HttpPut("{code}")]
        public async Task<ActionResult> UpdateLecture(string code, [FromBody] LectureUpdateDto lectureDto)
        {
            if (lectureDto == null)
                return BadRequest("LectureDto object is null.");

            await _serviceManager.LectureService.UpdateLectureAsync(code, lectureDto);
            return NoContent();
        }


        [HttpPost]
        [Route("api/lecture/assign")]
        // Örn: POST /api/LectureInstructor/assign?instructorId=5&lectureCode=BM401
        public async Task<IActionResult> AssignInstructorToLecture([FromQuery] int instructorId, [FromQuery] string lectureCode)
        {
            await _serviceManager.LectureService.CreateLectureInstructorAsync(instructorId, lectureCode);
            return Ok("Instructor assigned to lecture successfully.");
        }


        [HttpDelete]
        [Route("api/[controller]/unassign")]
        public async Task<ActionResult> DeleteLectureInstructor([FromRoute] int InstructorId, [FromBody] string lectureCode)
        {
            if (lectureCode == null)
                return BadRequest("Ders kodu null olamaz!");

            await _serviceManager.LectureService.DeleteLectureInstructorAsync(InstructorId, lectureCode);
            return NoContent();
        }

        // DELETE: api/Lectures/{code}
        [HttpDelete("{code}")]
        public async Task<ActionResult> DeleteLecture(string code)
        {
            await _serviceManager.LectureService.DeleteLectureAsync(code);
            return NoContent();
        }
    }
}
