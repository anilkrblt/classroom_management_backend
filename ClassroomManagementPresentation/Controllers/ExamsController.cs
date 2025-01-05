using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassroomManagementPresentation.Controllers
{
    [Route("api/exams")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public ExamsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: api/Exams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamDto>>> GetExams()
        {
            var exams = await _serviceManager.ExamService.GetAllExamsAsync(trackChanges: false);
            return Ok(exams);
        }

        // GET: api/Exams/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ExamDto>> GetExam(int id)
        {
            var exam = await _serviceManager.ExamService.GetExamByIdAsync(id, trackChanges: false);
            if (exam == null)
                return NotFound($"Exam with ID {id} not found.");

            return Ok(exam);
        }
       /* [HttpPost("examsessions")]
        public async Task<ExamSessionDto> CreateExamSessions(ExamSessionCreateDto dto){



        }*/

        // POST: api/Exams
        [HttpPost]
        public async Task<ActionResult> CreateExam([FromBody] ExamDto examDto)
        {
            if (examDto == null)
                return BadRequest("ExamDto object is null.");

            await _serviceManager.ExamService.CreateExamAsync(examDto);
            return CreatedAtAction(nameof(GetExam), new { id = examDto.ExamId }, examDto);
        }

        // PUT: api/Exams/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateExam(int id, [FromBody] ExamDto examDto)
        {
            if (examDto == null)
                return BadRequest("ExamDto object is null.");

            await _serviceManager.ExamService.UpdateExamAsync(id, examDto);
            return NoContent();
        }

        // DELETE: api/Exams/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteExam(int id)
        {
            await _serviceManager.ExamService.DeleteExamAsync(id);
            return NoContent();
        }
    }
}
