using Shared.DataTransferObjects;
using Service.Contracts;
using Contracts;
using AutoMapper;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;

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
    }
}
