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



        public async Task<List<ExamListDto>> CreateAllExamsAsync(ExamSessionCreateDto dto)
        {
            var lectures = await _repositoryManager.Lecture.GetAllLecturesAsync(false);
            foreach (var lecture in lectures)
            {
                var exam = new Exam
                {
                    Type = dto.Type,
                    Name = lecture.Name + $"Dersi {dto.Type} Sınavı",
                    LectureCode = lecture.Code,
                    Duration = 60
                };
                _repositoryManager.Exam.CreateExam(exam);
            }
            var postExams = await _repositoryManager.Exam.GetAllExamsAsync(false);

            var examList = _mapper.Map<List<ExamListDto>>(postExams);
            return examList;
        }



        public async Task<List<ExamScheduleDto>> CreateAllExamSessionsAsync(ExamSessionCreateDto dto)
        {
            var postExams = await _repositoryManager.Exam.GetAllExamsAsync(false);

            var examSessionPostdto = _mapper.Map<List<ExamSessionPostDto>>(postExams);

            var rooms = await _repositoryManager.Room.GetAllRoomsAsync(false);
            var examRooms = _mapper.Map<List<ExamRoomDto>>(rooms);
            var data = new ExamSessionCreate
            {
                exams = examSessionPostdto,
                rooms = examRooms
            };

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var url = "https://example.com/api/endpoint";
            using var client = new HttpClient();


            var response = await client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response: " + responseData);

                var examSchedule = JsonSerializer.Deserialize<List<ExamScheduleDto>>(responseData);
                return examSchedule;
            }
            else
            {
                Console.WriteLine("Failed: " + response.StatusCode);
            }
            throw new NotImplementedException();
        }
    }
}
