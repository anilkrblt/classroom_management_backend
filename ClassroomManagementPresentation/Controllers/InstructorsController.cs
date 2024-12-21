using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace class_management_backend.controllers
{
    [Route("api/instructors")]
    [ApiController]


    public class InstructorsController : ControllerBase
    {

        private readonly IServiceManager _servisManager;
        public InstructorsController(IServiceManager serviceManager)
        {
            _servisManager = serviceManager;
        }


        /*
        [HttpGet("department/{id:int}")]
        public async Task<ActionResult<IEnumerable<InstructorDTO>>> GetInstructorsByDepartmentId([FromRoute]int departmentId)
        {
            var instructors=_servisManager.InstructorService.
        }
        */
        [HttpGet("lecture/{id:int}")]
        public async Task<ActionResult<IEnumerable<InstructorDto>>> GetInstructorForLectureSession([FromRoute]int instructorId)
        {
            var instructor=_servisManager.InstructorService.GetInstructorForLectureSessionsAsync(instructorId,false);
            return Ok(instructor);
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<InstructorDto>> GetInstructor([FromRoute]int instructorId)
        {
            var instructor = await _servisManager.InstructorService.GetInstructorByIdAsync(instructorId, false);
            return Ok(instructor);
        }
    }
}