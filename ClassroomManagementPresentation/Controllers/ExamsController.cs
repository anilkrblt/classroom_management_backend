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

        [HttpPost]
        public async Task<List<ExamListDto>> CreateExams(ExamSessionCreateDto dto)
        {
            var exams = await _serviceManager.ExamService.CreateAllExamsAsync(dto);
            return exams;
        }

        [HttpPost("/examsessions")]
        public async Task<List<ExamScheduleDto>> CreateExamSessions(ExamSessionCreateDto dto)
        {

            var examShedule = await _serviceManager.ExamService.CreateAllExamSessionsAsync(dto);
            return examShedule;
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
