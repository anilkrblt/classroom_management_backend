using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

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
           var exams = await _servisManager.ExamService.GetExamsByIdsAsync
            return Ok(exams);
        }


        [HttpGet("student/{studentId:int}")]
        public async Task<IActionResult> GetExamsByStudentId(int studentId)
        {
        }



        [HttpPost("create")]
        public async Task<IActionResult> AddExam([FromBody] ExamDto request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    message = "Invalid input.",
                    errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                });
            }

            try
            {

                var lecture = await context.Lectures.FirstOrDefaultAsync(l => l.Code == request.Code);

                if (lecture == null)
                {
                    return NotFound(new { message = "Lecture not found." });
                }


                var exams = await context.Exams
                    .Where(e => e.Code == request.Code && e.ExamDate.Date == request.ExamDate.Date)
                    .ToListAsync();

                bool hasConflict = exams.Any(e =>
                {
                    var requestStartDateTime = request.ExamDate.Date.Add(request.StartTime);
                    var requestEndDateTime = request.ExamDate.Date.Add(request.EndTime);

                    return
                        (requestStartDateTime >= e.StartTime && requestStartDateTime < e.EndTime) ||
                        (requestEndDateTime > e.StartTime && requestEndDateTime <= e.EndTime) ||
                        (requestStartDateTime <= e.StartTime && requestEndDateTime >= e.EndTime);
                });

                if (hasConflict)
                {
                    return Conflict(new { message = "An exam for this lecture already exists during the specified time." });
                }



                var exam = mapper.Map<Exam>(request);
                await context.Exams.AddAsync(exam);
                await context.SaveChangesAsync();


                return Ok(new { message = "Exam added successfully.", examId = exam.ExamId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred.", error = ex.Message });
            }
        }




        [HttpPut("update/{examId:int}")]
        public async Task<IActionResult> UpdateExam(int examId, [FromBody] ExamDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    message = "Invalid input.",
                    errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                });
            }

            try
            {
                var exam = await context.Exams.FirstOrDefaultAsync(e => e.ExamId == examId);
                if (exam == null)
                {
                    return NotFound(new { message = "Exam not found." });
                }

                // Güncellenen tarih ve saat bilgilerini tam DateTime olarak hesapla
                var updatedStartTime = request.ExamDate.Date.Add(request.StartTime);
                var updatedEndTime = request.ExamDate.Date.Add(request.EndTime);

                // Çakışma kontrolü (diğer sınavlarla)
                var examsInConflict = await context.Exams
                    .Where(e =>
                        e.Code == exam.Code &&
                        e.ExamDate.Date == exam.ExamDate.Date &&
                        e.ExamId != examId) // Kendisini hariç tut
                    .ToListAsync();

                var hasConflict = examsInConflict.Any(e =>
                    (updatedStartTime >= e.StartTime && updatedStartTime < e.EndTime) ||
                    (updatedEndTime > e.StartTime && updatedEndTime <= e.EndTime) ||
                    (updatedStartTime <= e.StartTime && updatedEndTime >= e.EndTime));

                if (hasConflict)
                {
                    return Conflict(new { message = "An exam for this lecture already exists during the specified time." });
                }

                // Güncelleme işlemi
                exam.Code = request.Code;
                exam.ExamDate = request.ExamDate;
                exam.StartTime = updatedStartTime;
                exam.EndTime = updatedEndTime;

                await context.SaveChangesAsync();

                return Ok(new { message = "Exam updated successfully.", examId = exam.ExamId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the exam.", error = ex.Message });
            }
        }






    }
}
