using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace backend.controllers
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

        [HttpGet]
        public IActionResult GetAllStudent()
        {
            var sudents = context.Students;
            return Ok(sudents);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetStudentById([FromRoute(Name ="id")] int id)
        {
            var student = context.Students.Where(b => b.StudentId.Equals(id)).SingleOrDefault();

            if (student is null)
                return NotFound();
            return Ok(student);

        }

        [HttpGet("department/{departmentid:int}")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentsByDepartmentId(int departmentid)
        {

            var students = await context.Students.Where(s => s.DepartmentId==departmentid).ToListAsync();


            if(students is null || !students.Any())
            {
                return NotFound(new {message="No students found for this department."});
            }
            return Ok(students);
        }




    }
}
