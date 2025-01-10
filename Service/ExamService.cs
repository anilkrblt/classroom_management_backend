using Shared.DataTransferObjects;
using Service.Contracts;
using Contracts;
using AutoMapper;
using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;

namespace Service
{
    public class ExamService : IExamService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ExamService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        // Get all exams
        public async Task<IEnumerable<ExamDto>> GetAllExamsAsync(bool trackChanges)
        {
            var exams = await _repositoryManager.Exam.GetAllExamsAsync(trackChanges);
            return _mapper.Map<IEnumerable<ExamDto>>(exams);
        }

        // Get a specific exam by ID
        public async Task<ExamDto> GetExamByIdAsync(int examId, bool trackChanges)
        {
            var exam = await _repositoryManager.Exam.GetExamAsync(examId, trackChanges);
            if (exam == null)
                throw new KeyNotFoundException($"Exam with ID {examId} not found.");

            return _mapper.Map<ExamDto>(exam);
        }

        public async Task CreateExamAsync(ExamDto examDto)
        {
            var exam = _mapper.Map<Exam>(examDto);
            _repositoryManager.Exam.CreateExam(exam);
            await _repositoryManager.SaveAsync();
        }

        public async Task UpdateExamAsync(int examId, ExamDto examDto)
        {
            var exam = await _repositoryManager.Exam.GetExamAsync(examId, trackChanges: true);
            if (exam == null)
                throw new KeyNotFoundException($"Exam with ID {examId} not found.");

            _mapper.Map(examDto, exam);
            await _repositoryManager.SaveAsync();
        }

        public async Task DeleteExamAsync(int examId)
        {
            var exam = await _repositoryManager.Exam.GetExamAsync(examId, trackChanges: true);
            if (exam == null)
                throw new KeyNotFoundException($"Exam with ID {examId} not found.");

            await _repositoryManager.Exam.DeleteExamAsync(exam);
            await _repositoryManager.SaveAsync();
        }



        public async Task<List<ExamListDto>> CreateAllExamsAsync(string type)
        {
            var lectures = await _repositoryManager.Lecture.GetAllLecturesAsync(false);
            foreach (var lecture in lectures)
            {
                var exam = new Exam
                {
                    Type = type,
                    Name = lecture.Name + $"Dersi {type} Sınavı",
                    LectureCode = lecture.Code,
                    Duration = 60
                };
                _repositoryManager.Exam.CreateExam(exam);
            }
            await _repositoryManager.SaveAsync();
            var postExams = await _repositoryManager.Exam.GetAllExamsAsync(false);

            var examList = _mapper.Map<List<ExamListDto>>(postExams);
            return examList;
        }



        public async Task<List<ExamScheduleDto>> CreateAllExamSessionsAsync(ExamSessionCreateDto dto)
        {
            var postExams = await _repositoryManager.Exam.GetAllExamsAsync(false);
            foreach (var exam in postExams)
            {
                // İlgili ders var mı kontrol edelim
                if (exam.Lecture != null)
                {
                    var lecture = exam.Lecture;

                    if (lecture.DepartmentId == 2)
                    {
                        lecture.Grade += 4;
                    }
                    else if (lecture.DepartmentId == 3)
                    {
                        lecture.Grade += 8;
                    }
                    else if (lecture.DepartmentId == 4)
                    {
                        lecture.Grade += 12;
                    }
                    else if (lecture.DepartmentId == 5)
                    {
                        lecture.Grade += 16;
                    }
                }
            }

            var examSessionPostdto = _mapper.Map<List<ExamSessionPostDto>>(postExams);



            var rooms = await _repositoryManager.Room.GetAllRoomsAsync(false);
            var examRooms = _mapper.Map<List<ExamRoomDto>>(rooms);


            var examDates = new List<ExamDateRangeDto> { };
            var examHours = new List<string>
                {
                    "09:00","09:30","10:00","10:30","11:00","11:30",
                    "13:00","13:30","14:00","14:30","15:00","15:30",
                    "16:00","16:30","17:00","17:30"
                };
            foreach (var date in dto.Dates)
            {
                var item = new ExamDateRangeDto
                {
                    Tarih = date,
                    BaslangicSaatleri = examHours
                };
                examDates.Add(item);

            }
            var data = new ExamSessionCreate
            {
                Exams = examSessionPostdto,
                Rooms = examRooms,
                DateRange = examDates
            };

            var json = JsonSerializer.Serialize(data);

            Console.WriteLine("********** JSON to send **********");
            Console.WriteLine(json);
            Console.WriteLine("**********************************");

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var url = "http://127.0.0.1:8000/planning";
            using var client = new HttpClient();


            var response = await client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response: " + responseData);

                var examSchedule = JsonSerializer.Deserialize<List<ExamScheduleDto>>(responseData);
                if (examSchedule is not null)
                    return examSchedule;
                return new List<ExamScheduleDto> { };
            }
            else
            {
                Console.WriteLine("Failed: " + response.StatusCode);
                throw new Exception($"Exam schedule creation failed with status: {response.StatusCode}");

            }
        }
    }
}
