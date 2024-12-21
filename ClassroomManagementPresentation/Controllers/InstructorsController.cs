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



        [HttpGet("department/{id:int}")]
        public async Task<ActionResult<IEnumerable<InstructorDTO>>> GetInstructorsByDepartmentId([FromRoute]int departmentId)
        {
            var instructors=_servisManager.InstructorService.Get
        }
    }
}