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

        public async Task<IEnumerable<ExamDto>> GetAllExamsAsync(bool trackChanges)
        {
            var exams = await _repositoryManager.Exam.GetAllExamsAsync(trackChanges);
            return _mapper.Map<IEnumerable<ExamDto>>(exams);
        }

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



        public async Task<List<ExamListDto>> CreateAllExamsAsync(ExamCreateDto dto)
        {
            var lectures = await _repositoryManager.Lecture.GetAllLecturesAsync(false);
            lectures = lectures.Where(l => l.Term == dto.Term);
            foreach (var lecture in lectures)
            {
                var exam = new Exam
                {
                    Type = dto.Type,
                    Name = lecture.Name + $"Dersi {dto.Type} Sınavı",
                    Year = dto.Year,
                    LectureCode = lecture.Code,
                    Duration = 60
                };
                _repositoryManager.Exam.CreateExam(exam);
            }
            await _repositoryManager.SaveAsync();
            var postExams = await _repositoryManager.Exam.GetAllExamsAsync(false);
            postExams = postExams.Where(e => e.Year == dto.Year && e.Type == dto.Type);

            var examList = _mapper.Map<List<ExamListDto>>(postExams);
            return examList;
        }

        public async Task<ExamScheduleExtendedAndMoreDto> CreateAllExamSessionsAsync(ExamSessionCreateDto dto)
        {
            var postExams = await _repositoryManager.Exam.GetAllExamsAsync(false);
            postExams = postExams.Where(e => e.Year == dto.Year);
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
            var url = "https://algoritma.onrender.com/planning";
            using var client = new HttpClient();


            var response = await client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response: " + responseData);

                var examSchedule = JsonSerializer.Deserialize<ExamScheduleAndMoreDto>(responseData);

                var examScheduleExtended = new List<ExamScheduleExtendedDto>();
                var unAssignedLectures = new List<UnAssignedLecturesDto>();

                var lectures = await _repositoryManager.Lecture.GetAllLecturesAsync(false);
                foreach (var exam in examSchedule!.ExamSchedule)
                {
                    var lecture = lectures.Where(l => l.Code == exam.LectureCode).FirstOrDefault();
                    var item = new ExamScheduleExtendedDto
                    {
                        Date = exam.Date,
                        Duration = exam.Duration,
                        StartTime = exam.StartTime,
                        EndTime = exam.EndTime,
                        RoomNames = exam.RoomNames,
                        LectureCode = exam.LectureCode,
                        LectureName = lecture!.Name,
                        DepartmentName = lecture.Department.Name,
                        Grade = lecture.Grade,
                    };
                    CreateExamSession(item, dto.Term, dto.Type, exam.LectureCode, dto.Year);
                    examScheduleExtended.Add(item);
                }
                await _repositoryManager.SaveAsync();
                foreach (var lecCode in examSchedule!.UnAssignedLectures)
                {
                    var lecture = lectures.Where(l => l.Code == lecCode).FirstOrDefault();
                    var item = new UnAssignedLecturesDto
                    {
                        LectureCode = lecCode,
                        LectureName = lecture!.Name,
                        DepartmentName = lecture.Department.Name,
                        Grade = lecture.Grade,
                    };
                    unAssignedLectures.Add(item);

                }

                if (examSchedule is not null)
                    return new ExamScheduleExtendedAndMoreDto
                    {
                        ExamSchedule = examScheduleExtended,
                        UnAssignedLectures = unAssignedLectures
                    };
                return new ExamScheduleExtendedAndMoreDto { };
            }


            else
            {
                Console.WriteLine("Failed: " + response.StatusCode);
                throw new Exception($"Exam schedule creation failed with status: {response.StatusCode}");

            }
        }

        public async Task DeleteAllExamSessionsAsync(ExamSessionDeleteDto dto)
        {
            var exams = await _repositoryManager.ExamSession.GetAllExamSessionsAsync(true);
            var examList = exams.Where(e => dto.Year == e.Exam.Year && dto.Type == e.Exam.Type && dto.Term == e.Exam.Lecture.Term).ToList();
            Console.WriteLine("Exam List:");
            foreach (var exam in examList)
            {
                Console.WriteLine($"ExamSessionId: {exam.ExamSessionId}, ExamDate: {exam.ExamDate}, Term: {exam.Exam.Lecture.Term}");
            }

            // Boş kontrolü
            if (!examList.Any())
            {
                Console.WriteLine("Exam listesi boş. Silinecek bir şey yok.");
                return;
            }
            _repositoryManager.ExamSession.DeleteAllExamSessions(examList);
            await _repositoryManager.SaveAsync();
        }
        
        public async Task CreateExamSession(ExamScheduleExtendedDto dto, string Term, string Type, string LectureCode, int ExamYear)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "DTO null olamaz.");
            }

            if (string.IsNullOrWhiteSpace(dto.RoomNames))
            {
                throw new ArgumentException("RoomNames null veya boş olamaz.");
            }

            var examRooms = dto.RoomNames.Split(',')
                                         .Select(r => r.Trim())
                                         .Where(r => !string.IsNullOrEmpty(r))
                                         .ToList();

            var exam = await _repositoryManager.Exam.GetExamWithLectureCodeAsync(Term, Type, LectureCode, ExamYear, false);
            if (exam == null)
            {
                throw new InvalidOperationException("Belirtilen kriterlere uygun bir sınav bulunamadı.");
            }

            foreach (var roomName in examRooms)
            {
                var room = await _repositoryManager.Room.GetRoomByNameAsync(roomName, false);
                if (room == null)
                {
                    throw new InvalidOperationException($"'{roomName}' adlı oda bulunamadı.");
                }

                if (DateTime.TryParse(dto.Date, out DateTime examDate) &&
                    TimeSpan.TryParse(dto.StartTime, out TimeSpan startTime) &&
                    TimeSpan.TryParse(dto.EndTime, out TimeSpan endTime))
                {
                    var examSession = new ExamSession
                    {
                        ExamDate = examDate,
                        StartTime = startTime,
                        EndTime = endTime,
                        RoomId = room.RoomId,
                        ExamId = exam.ExamId
                    };

                    _repositoryManager.ExamSession.CreateExamSession(examSession);
                }
                else
                {
                    throw new ArgumentException("Geçersiz tarih veya saat formatı.");
                }
            }

            await _repositoryManager.SaveAsync();
        }

    }
}
