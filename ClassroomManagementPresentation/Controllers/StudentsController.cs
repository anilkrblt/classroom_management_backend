using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassroomManagementPresentation.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public StudentsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudents()
        {
            var students = await _serviceManager.StudentService.GetAllStudentsAsync(trackChanges: false);
            return Ok(students);
        }

        // GET: api/Students/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> GetStudent(int id)
        {
            var student = await _serviceManager.StudentService.GetStudentByIdAsync(id, trackChanges: false);

            if (student == null)
                return NotFound($"Student with ID {id} not found.");

            return Ok(student);
        }

        // GET: api/Students/{id}/lectures
        [HttpGet("{id}/lectures")]
        public async Task<ActionResult<IEnumerable<LectureDto>>> GetStudentLectures(int id)
        {
            var lectures = await _serviceManager.StudentService.GetStudentLecturesAsync(id, trackChanges: false);

            if (lectures == null || !lectures.Any())
                return NotFound($"No lectures found for Student with ID {id}.");

            return Ok(lectures);
        }

        // GET: api/Students/{id}/clubs
        [HttpGet("{id}/clubs")]
        public async Task<ActionResult<IEnumerable<ClubDto>>> GetStudentClubs(int id)
        {
            var clubs = await _serviceManager.StudentService.GetStudentClubsAsync(id, trackChanges: false);

            if (clubs == null || !clubs.Any())
                return NotFound($"No clubs found for Student with ID {id}.");

            return Ok(clubs);
        }

        // GET: api/Students/{id}/exams
        [HttpGet("{id}/exams")]
        public async Task<ActionResult<IEnumerable<ExamDto>>> GetStudentExams(int id)
        {
            var exams = await _serviceManager.StudentService.GetStudentExamsAsync(id, trackChanges: false);

            if (exams == null || !exams.Any())
                return NotFound($"No exams found for Student with ID {id}.");

            return Ok(exams);
        }

        // GET: api/Students/ByDepartment/{departmentId}
        [HttpGet("ByDepartment/{departmentId}")]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudentsByDepartment(int departmentId)
        {
            var students = await _serviceManager.StudentService.GetStudentsByDepartmentAsync(departmentId, trackChanges: false);

            if (students == null || !students.Any())
                return NotFound($"No students found in Department with ID {departmentId}.");

            return Ok(students);
        }

        // POST: api/Students
        [HttpPost]
        public async Task<ActionResult> CreateStudent([FromBody] StudentDto studentDto)
        {
            if (studentDto == null)
                return BadRequest("StudentDto object is null.");

            await _serviceManager.StudentService.CreateStudentAsync(studentDto);
            return CreatedAtAction(nameof(GetStudent), new { id = studentDto.StudentId }, studentDto);
        }

        // PUT: api/Students/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStudent(int id, [FromBody] StudentDto studentDto)
        {
            if (studentDto == null)
                return BadRequest("StudentDto object is null.");

            await _serviceManager.StudentService.UpdateStudentAsync(id, studentDto);
            return NoContent();
        }

        // DELETE: api/Students/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            await _serviceManager.StudentService.DeleteStudentAsync(id);
            return NoContent();
        }
    }
}
