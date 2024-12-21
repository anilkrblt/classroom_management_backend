using Entities.Models;
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
            var sudents = _serviceManager.StudentService.GetAllStudent(false);
            return Ok(sudents);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetStudentById([FromRoute(Name ="id")] int id)
        {
            var student = _serviceManager.StudentService.GetStudentById(id, false);
            return Ok(student);
        }


        [HttpGet("department/{departmentid:int}")]
        public ActionResult<IEnumerable<Student>> GetStudentsByDepartmentId(int departmentid)
        {

            var students = _serviceManager.StudentService.GetStudentsByDepartmentId(departmentid, false);
            return Ok(students);
        }

    }
}
