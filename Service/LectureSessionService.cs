using Shared.DataTransferObjects;
using Service.Contracts;
using Contracts;
using AutoMapper;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using GeneticAlgorithmExample;

namespace Service
{
    public class LectureSessionService : ILectureSessionService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public LectureSessionService(IRepositoryManager repositoryManager, IMapper mapper, ILoggerManager logger)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<LectureSessionDto>> GetAllLectureSessionsAsync(bool trackChanges)
        {
            var lectureSessions = await _repositoryManager.LectureSession.GetAllLectureSessionsAsync(trackChanges);
            return _mapper.Map<IEnumerable<LectureSessionDto>>(lectureSessions);
        }

        public async Task<LectureSessionDto> GetLectureSessionByIdAsync(int lectureSessionId, bool trackChanges)
        {
            var lectureSession = await _repositoryManager.LectureSession.GetLectureSessionAsync(lectureSessionId, trackChanges);

            if (lectureSession == null)
                throw new KeyNotFoundException($"LectureSession with ID {lectureSessionId} not found.");

            return _mapper.Map<LectureSessionDto>(lectureSession);
        }

        public async Task<IEnumerable<LectureSessionDto>> GetLectureSessionsByInstructorIdAsync(int instructorId, bool trackChanges)
        {
            var lectureSessions = await _repositoryManager.LectureSession.GetLectureSessionByInstructorIdAsync(instructorId, trackChanges);

            if (!lectureSessions.Any())
                throw new KeyNotFoundException($"No lecture sessions found for Instructor with ID {instructorId}.");

            return _mapper.Map<IEnumerable<LectureSessionDto>>(lectureSessions);
        }

        public async Task CreateLectureSessionAsync(LectureSessionDto lectureSessionDto)
        {
            var lectureSession = _mapper.Map<LectureSession>(lectureSessionDto);

            _repositoryManager.LectureSession.CreateLectureSession(lectureSession);
            await _repositoryManager.SaveAsync();
            _logger.LogInfo($"{lectureSessionDto.LectureCode} ders kodlu dersiniz {lectureSessionDto.RoomName} isimli odaya eklenmiştir.");
        }

        public async Task UpdateLectureSessionAsync(int lectureSessionId, LectureSessionUpdateDto lectureSessionDto)
        {
            var lectureSession = await _repositoryManager.LectureSession.GetLectureSessionAsync(lectureSessionId, trackChanges: true);

            if (lectureSession == null)
                throw new KeyNotFoundException($"LectureSession with ID {lectureSessionId} not found.");
            var room = await _repositoryManager.Room.GetRoomByNameAsync(lectureSessionDto.RoomName, false);
            if (room == null)
                throw new KeyNotFoundException($"room not found.");
            lectureSession.Date = lectureSessionDto.Date.Date;
            lectureSession.StartTime = lectureSessionDto.StartTime;
            lectureSession.EndTime = lectureSessionDto.EndTime;
            lectureSession.RoomId = room.RoomId;
            await _repositoryManager.SaveAsync();


            var enrollments = await _repositoryManager.Enrollment.GetAllEnrollmentsAsync(false);
            var students = enrollments.Where(e => e.LectureCode == lectureSession.LectureCode).Select(e => e.Student);


            var notif = new Notification
            {
                CreatedAt = DateTime.Now,
                Title = $"{lectureSession.LectureCode} dersiniz güncellenmiştir!",
                NotificationType = "LectureUpdateBildirimi",
                Message = $"dersine bişiler oldu la   !! "

            };
            _repositoryManager.Notification.CreateNotification(notif);
            await _repositoryManager.SaveAsync();

            foreach (var student in students)
            {

                var notifRecive = new NotificationRecipient
                {
                    IsRead = false,
                    NotificationId = notif.NotificationId,
                    ReadAt = DateTime.MinValue,
                    UserId = student.UserId
                };
                _repositoryManager.NotificationRecipient.CreateNotificationRecipient(notifRecive);
            }
            await _repositoryManager.SaveAsync();
        }

        public async Task DeleteLectureSessionAsync(int lectureSessionId)
        {
            var lectureSession = await _repositoryManager.LectureSession.GetLectureSessionAsync(lectureSessionId, trackChanges: true);

            if (lectureSession == null)
                throw new KeyNotFoundException($"LectureSession with ID {lectureSessionId} not found.");

            await _repositoryManager.LectureSession.DeleteLectureSessionAsync(lectureSession);
            await _repositoryManager.SaveAsync();

            var enrollments = await _repositoryManager.Enrollment.GetAllEnrollmentsAsync(false);
            var students = enrollments.Where(e => e.LectureCode == lectureSession.LectureCode).Select(e => e.Student);


            var notif = new Notification
            {
                CreatedAt = DateTime.Now,
                Title = $"{lectureSession.LectureCode} dersiniz iptal edilmiştir!",
                NotificationType = "LectureUpdateBildirimi",
                Message = $"dersine bişiler oldu la   !! "

            };
            _repositoryManager.Notification.CreateNotification(notif);
            await _repositoryManager.SaveAsync();

            foreach (var student in students)
            {

                var notifRecive = new NotificationRecipient
                {
                    IsRead = false,
                    NotificationId = notif.NotificationId,
                    ReadAt = DateTime.MinValue,
                    UserId = student.UserId
                };
                _repositoryManager.NotificationRecipient.CreateNotificationRecipient(notifRecive);
            }
            await _repositoryManager.SaveAsync();
            _logger.LogWarn($"{lectureSession.LectureCode} - {lectureSession.Date} dersiniz silinmiştir.");

        }

        public async Task CreateSchedule()
        {
            /*
            var rooms = await _repositoryManager.Room.GetAllRoomsAsync(false);
            var classrooms = rooms.Select(r => r.Name).ToList();

            var instructors = await _repositoryManager.Instructor.GetAllInstructorsAsync(false);
            var instructorPreferences = await _repositoryManager.InstructorPreference.GetAllInstructorPreferencesAsync(false);
            var lectureInstructors = await _repositoryManager.LectureInstructor.GetAllInstructorLecturesAsync(false);
            var lectures = await _repositoryManager.Lecture.GetAllLecturesAsync(false);
            lectures = lectures.OrderBy(l => l.Grade);



            var newSchedule = new Dictionary<string, string>();
            foreach (var lecture in lectures)
            {
                var instrcs = lectureInstructors.Where(li => li.LectureCode == lecture.Code).Select(li => li.Instructor).ToList();
                if (instrcs.Count > 1)
                {
                    var i = 0;
                    foreach (var ins in instrcs)
                    {
                        if (i == 1)
                        {
                            var ip = instructorPreferences.Where(ip => ip.InstructorId == ins.InstructorId && lecture.Code == ip.LectureCode).FirstOrDefault();

                            var key = $"{lecture.Code} {ins.Name} {lecture.Name} ({lecture.Grade}A)";
                            var value = ip!.PreferredTime;
                            newSchedule[key] = value;
                        }
                        if (i == 2)
                        {
                            var ip = instructorPreferences.Where(ip => ip.InstructorId == ins.InstructorId && lecture.Code == ip.LectureCode).FirstOrDefault();

                            var key = $"{lecture.Code} {ins.Name} {lecture.Name} ({lecture.Grade}B)";
                            var value = ip!.PreferredTime;
                            newSchedule[key] = value;
                        }

                        i++;
                    }
                }
                else
                {
                    var ins = instrcs.FirstOrDefault();
                    var ip = instructorPreferences.Where(ip => ip.InstructorId == ins!.InstructorId && lecture.Code == ip.LectureCode).FirstOrDefault();
                    var key = $"{lecture.Code} {ins!.Name} {lecture.Name} ({lecture.Grade}AB)";
                    var value = ip!.PreferredTime;
                    newSchedule[key] = value;
                }


            }

            var newProjectIndexes = newSchedule
             .Select((kvp, index) => new { kvp, index })
             .Where(x => x.kvp.Key.ToLower().Contains("proje"))
             .Select(x => x.index)
             .ToList();

            var indexedSchedule = newSchedule
                .Select((kvp, index) => new { Key = kvp.Key, Value = kvp.Value, Index = index })
                .ToList();

            var grade1List = new List<int>();
            var grade2List = new List<int>();
            var grade3List = new List<int>();

            var newFourthGradeAndIndexes = new List<int>();


            var newBranchAndIndexesTemp = new Dictionary<string, List<int>>
            {
                {"1A" , new List<int>()},
                {"1B" , new List<int>()},
                {"1AB" , new List<int>()},
                {"2A" , new List<int>()},
                {"2B" , new List<int>()},
                {"2AB" , new List<int>()},
                {"3A" , new List<int>()},
                {"3B" , new List<int>()},
                {"3AB" , new List<int>()},
            };



            foreach (var item in indexedSchedule)
            {
                var keyLower = item.Key.ToLower();

                if (keyLower.Contains("(4ab)"))
                {
                    newFourthGradeAndIndexes.Add(item.Index);
                }
                else if (keyLower.Contains("(1ab)") || keyLower.Contains("(1a)") || keyLower.Contains("(1b)"))
                {
                    grade1List.Add(item.Index);
                    if (keyLower.Contains("(1ab)"))
                        newBranchAndIndexesTemp["1AB"].Add(item.Index);
                    if (keyLower.Contains("(1a)"))
                        newBranchAndIndexesTemp["1A"].Add(item.Index);
                    if (keyLower.Contains("(1b)"))
                        newBranchAndIndexesTemp["1B"].Add(item.Index);

                }
                else if (keyLower.Contains("(2ab)") || keyLower.Contains("(2a)") || keyLower.Contains("(2b)"))
                {
                    grade2List.Add(item.Index);
                    if (keyLower.Contains("(2ab)"))
                        newBranchAndIndexesTemp["2AB"].Add(item.Index);
                    if (keyLower.Contains("(2a)"))
                        newBranchAndIndexesTemp["2A"].Add(item.Index);
                    if (keyLower.Contains("(2b)"))
                        newBranchAndIndexesTemp["2B"].Add(item.Index);
                }

                else if (keyLower.Contains("(3ab)") || keyLower.Contains("(3a)") || keyLower.Contains("(3b)"))
                {
                    grade3List.Add(item.Index);
                    if (keyLower.Contains("(3ab)"))
                        newBranchAndIndexesTemp["3AB"].Add(item.Index);
                    if (keyLower.Contains("(3a)"))
                        newBranchAndIndexesTemp["3A"].Add(item.Index);
                    if (keyLower.Contains("(3b)"))
                        newBranchAndIndexesTemp["3B"].Add(item.Index);
                }


            }



            var newBranchAndIndexes = newBranchAndIndexesTemp.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.ToArray()
            );
            var newGradesAndIndexes = new Dictionary<string, int[]>
            {
                { "1", grade1List.ToArray() },
                { "2", grade2List.ToArray() },
                { "3", grade3List.ToArray() }
            };
            //

            indexedSchedule = newSchedule
                .Select((kvp, index) => new { Key = kvp.Key, Value = kvp.Value, Index = index })
                .ToList();


            var instructorEntries = indexedSchedule
                .Select(x => new
                {
                    InstructorName = ExtractInstructorName(x.Key),
                    Index = x.Index
                })
                .Where(x => !string.IsNullOrWhiteSpace(x.InstructorName))
                .ToList();

            var newInstructorsAndIndexes = instructorEntries
                .GroupBy(x => x.InstructorName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.Index)
                         .OrderBy(i => i)
                         .ToArray()
                );

            var newInstructorAndPreferences = new List<List<int>>();

            var days = Data.days;


            foreach (var kvp in newSchedule)
            {

                var lastSpaceIndex = kvp.Value.LastIndexOf(' ');
                if (lastSpaceIndex < 0)
                {
                    continue;
                }

                var room = kvp.Value.Substring(lastSpaceIndex + 1).Trim();  
                var roomIndex = classrooms.IndexOf(room);
                if (roomIndex < 0)
                {
                    continue;
                }

                var daySlot = kvp.Value.Substring(0, lastSpaceIndex).Trim();
                var dayIndex = Data.days.IndexOf(daySlot);
                if (dayIndex < 0)
                {
                    continue;
                }
                int combinedIndex = dayIndex * Data.classrooms.Count + roomIndex;
                newInstructorAndPreferences.Add(new List<int> { combinedIndex });
            }
            Data.UpdateAllData(
                newInstructorAndPreferences,
                newInstructorsAndIndexes,
                newBranchAndIndexes,
                newGradesAndIndexes,
                newFourthGradeAndIndexes,
                newProjectIndexes,
                newSchedule,
                classrooms
            );

*/
            int geneCount = 94;
            int minValue = 0;
            int maxValue = 179;
            int populationSize = 30;
            int generations = 1000;
            double mutationRate = 0.7;
            double crossoverRate = 0.75;

            Func<Chromosome, double> fitnessFunction = (chromosome) =>
            {
                return PenaltyFunctions.PenaltyForInstructorConflict(chromosome.Genes.ToList())
                     + PenaltyFunctions.PenaltyForOtherGrades(chromosome.Genes.ToList())
                     + PenaltyFunctions.PenaltyForFourthGrade(chromosome.Genes.ToList())
                     + PenaltyFunctions.PenaltyForInstructorPreferences(chromosome.Genes.ToList());
            };

            var ga = new GeneticAlgorithm(geneCount, minValue, maxValue, mutationRate,
                                          crossoverRate, fitnessFunction, populationSize);

            ga.DriveGA(generations);
            ga.ShowBestSolution();

            _logger.LogInfo("CreateSchedule -> GA çalıştırıldı ve en iyi çözüm üretildi.");
        }

        private string ExtractInstructorName(string key)
        {
            List<string> words = key.Split(" ").ToList();
            return $"{words[1]} {words[2]}";
        }

    }
}
