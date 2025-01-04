using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassroomManagementPresentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public DepartmentController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: api/Departments
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetDepartments()
        {
            var departments = await _serviceManager.DepartmentService.GetAllDepartmentsAsync(trackChanges: false);
            return Ok(departments);
        }

        // GET: api/Departments/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDto>> GetDepartment(int id)
        {
            var department = await _serviceManager.DepartmentService.GetDepartmentByIdAsync(id, trackChanges: false);

            if (department == null)
                return NotFound($"Department with ID {id} not found.");

            return Ok(department);
        }

        // GET: api/Departments/{id}/instructors
        [HttpGet("{id}/instructors")]
        public async Task<ActionResult<IEnumerable<InstructorDto>>> GetDepartmentInstructors(int id)
        {
            var instructors = await _serviceManager.DepartmentService.GetDepartmentInstructorsAsync(id, trackChanges: false);

            if (instructors == null || !instructors.Any())
                return NotFound($"No instructors found for Department with ID {id}.");

            return Ok(instructors);
        }

        // GET: api/Departments/{id}/rooms
        [HttpGet("{id}/rooms")]
        public async Task<ActionResult<IEnumerable<RoomDto>>> GetDepartmentRooms(int id)
        {
            var rooms = await _serviceManager.DepartmentService.GetDepartmentRoomsAsync(id, trackChanges: false);

            if (rooms == null || !rooms.Any())
                return NotFound($"No rooms found for Department with ID {id}.");

            return Ok(rooms);
        }

        // GET: api/Departments/{id}/students
        [HttpGet("{id}/students")]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetDepartmentStudents(int id)
        {
            var students = await _serviceManager.DepartmentService.GetDepartmentStudentsAsync(id, trackChanges: false);

            if (students == null || !students.Any())
                return NotFound($"No students found for Department with ID {id}.");

            return Ok(students);
        }

        // POST: api/Departments
        [HttpPost]
        public async Task<ActionResult> CreateDepartment([FromBody] DepartmentForCreateDto departmentDto)
        {
            if (departmentDto == null)
                return BadRequest("DepartmentDto object is null.");

            await _serviceManager.DepartmentService.CreateDepartmentAsync(departmentDto);
            return Created();
        }

        // PUT: api/Departments/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDepartment(int id, [FromBody] DepartmentForUpdateDto departmentDto)
        {
            if (departmentDto == null)
                return BadRequest("DepartmentDto object is null.");

            await _serviceManager.DepartmentService.UpdateDepartmentAsync(id, departmentDto);
            return NoContent();
        }

        // DELETE: api/Departments/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDepartment(int id)
        {
            await _serviceManager.DepartmentService.DeleteDepartmentAsync(id);
            return NoContent();
        }
    }
}
