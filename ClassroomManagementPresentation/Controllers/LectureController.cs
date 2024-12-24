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
        public async Task<ActionResult> CreateLecture([FromBody] LectureDto lectureDto)
        {
            if (lectureDto == null)
                return BadRequest("LectureDto object is null.");

            await _serviceManager.LectureService.CreateLectureAsync(lectureDto);
            return CreatedAtAction(nameof(GetLecture), new { code = lectureDto.Code }, lectureDto);
        }

        // PUT: api/Lectures/{code}
        [HttpPut("{code}")]
        public async Task<ActionResult> UpdateLecture(string code, [FromBody] LectureDto lectureDto)
        {
            if (lectureDto == null)
                return BadRequest("LectureDto object is null.");

            await _serviceManager.LectureService.UpdateLectureAsync(code, lectureDto);
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
