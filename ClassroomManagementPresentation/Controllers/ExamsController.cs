using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace class_management_backend.controllers
{
    [Route("api/exams")]
    [ApiController]


    public class ExamsController : ControllerBase
    {

        private readonly IServiceManager _servisManager;
        public ExamsController(IServiceManager serviceManager)
        {
            _servisManager = serviceManager;
        }


        [HttpGet()]
        public async Task<IActionResult> GetAllExams()
        {
            var exams = await _servisManager.ExamService.GetAllExamsAsync(false);
            return Ok(exams);
        }

        [HttpGet("department/{id:int}")]
        public async Task<IActionResult> GetExamsByDepartmentId([FromRoute(Name ="id")]int departmentId)
        {
            var exams = await _servisManager.ExamService.GetExamsByDepartmentId(departmentId,false);
            return Ok(exams);
        }


        [HttpGet("student/{studentId:int}")]
        public async Task<IActionResult> GetExamsByStudentId(int studentId,bool trackChanges)
        {
            var exams=_servisManager.ExamService.GetExamsByStudentIdAsync(studentId,trackChanges);
            return Ok(exams);
        }



        [HttpPost("create")]
        public async Task<IActionResult> CreateExam([FromBody] ExamDto request)
        {
            var exam=_servisManager.ExamService.CreateExam(request,true);
            return Ok(exam);
        }




        [HttpPut("update/{examId:int}")]
        public async Task<IActionResult> UpdateExam(int examId, [FromBody] ExamDto request)
        {
            var exam=_servisManager.ExamService.UpdateExamAsync(examId,request,false);
            return Ok(exam);
        }






    }
}