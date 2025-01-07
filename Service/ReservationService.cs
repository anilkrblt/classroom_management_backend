using Shared.DataTransferObjects;
using Service.Contracts;
using Contracts;
using AutoMapper;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.ObjectPool;

namespace Service
{
    public class ReservationService : IReservationService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ReservationService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReservationDto>> GetAllReservationsAsync(bool trackChanges)
        {
            var reservations = await _repositoryManager.Reservation.GetAllReservationsAsync(trackChanges);
            return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }

        public async Task<IEnumerable<ClubReservationGetDto>> GetAllClubReservations(bool trackChanges)
        {
            var ClubReservations = await _repositoryManager.ClubReservation.GetAllClubReservationsAsync(trackChanges);
            if (ClubReservations is null)
                throw new KeyNotFoundException($"club reservation not found.");

            var reservationDtos = _mapper.Map<IEnumerable<ClubReservationGetDto>>(ClubReservations);

            return reservationDtos;

        }

        public async Task<ReservationDto> GetReservationByIdAsync(int reservationId, bool trackChanges)
        {
            var reservation = await _repositoryManager.Reservation.GetReservationAsync(reservationId, trackChanges);

            if (reservation == null)
                throw new KeyNotFoundException($"Reservation with ID {reservationId} not found.");

            return _mapper.Map<ReservationDto>(reservation);
        }

        public async Task<IEnumerable<ReservationDto>> GetUserReservationsAsync(int userId, bool trackChanges)
        {
            var reservations = await _repositoryManager.Reservation.GetReservationsByUserId(userId, trackChanges);

            if (!reservations.Any())
                throw new KeyNotFoundException($"No reservations found for User with ID {userId}.");

            return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }

        public async Task CreateReservationAsync(ReservationDto reservationDto)
        {
            var reservation = _mapper.Map<Reservation>(reservationDto);

            _repositoryManager.Reservation.CreateReservation(reservation);
            await _repositoryManager.SaveAsync();
        }

        public async Task<ClubReservationDto> CreateClubReservationAsync(ClubReservationDto reservationDto)
        {

            // 1) İlgili club var mı?
            var club = await _repositoryManager.Club
                .GetClubByNameAsync(reservationDto.ClubName, trackChanges: false);
            if (club == null)
                throw new KeyNotFoundException($"Club '{reservationDto.ClubName}' not found.");
            // Eğer yoksa create edecekseniz, ek kod yazıp kaydedebilirsiniz.

            // 2) Room bulma / oluşturma (opsiyonel).
            //    "reservationDto.RoomName" verisine göre "Room" tablosu aranabilir.
            var room = await _repositoryManager.Room.GetRoomByNameAsync(reservationDto.RoomName, trackChanges: false);
            if (room == null)
                throw new KeyNotFoundException($"Room '{reservationDto.RoomName}' not found.");
            // Veya "CreateRoom" diyebilirsiniz.

            // 3) Reservation nesnesi oluştur
            //    StartTime, EndTime, EventDate gibi alanlar set ediliyor.
            var newReservation = new Reservation
            {
                StartTime = reservationDto.StartTime,
                EndTime = reservationDto.EndTime,
                EventDate = reservationDto.EventTime.Date,
                // Örn. EventTime'ın sadece tarihi
                RoomId = room.RoomId
            };
            _repositoryManager.Reservation.CreateReservation(newReservation);

            // 4) ClubReservation nesnesi oluştur
            var clubReservation = new ClubReservation
            {
                // Key alanlar
                ClubId = club.ClubId,    // FK
                StudentId = reservationDto.StudentId,  // Student tablosuna FK
                Reservation = newReservation, // Bağlantı (ReservationId set edilecek)

                // Diğer alanlar
                EventRegisterLink = reservationDto.Link,
                Title = reservationDto.Title,
                Details = reservationDto.Details,
                Status = reservationDto.Status,
                Banner = reservationDto.BannerPath,

                // İsterseniz "EventName" -> reservationDto.Title ya da benzeri
                EventName = reservationDto.Title
            };
            _repositoryManager.ClubReservation.CreateClubReservation(clubReservation);

            // 5) Tüm eklemeler DB'ye
            await _repositoryManager.SaveAsync();

            // 6) Geriye Dönüş
            // Kaydedilen veriden "Id" vb. de dahil edip, isterseniz geri DTO'yu zenginleştirebilirsiniz.
            var returnDto = new ClubReservationDto
            {
                StudentId = clubReservation.StudentId,
                ClubName = club.Name,
                RoomName = room.Name,
                StartTime = newReservation.StartTime,
                EndTime = newReservation.EndTime,
                EventTime = newReservation.EventDate, // Sadece günü tuttuk, 
                                                      // dilerseniz EndTime + StartTime'dan hesap
                Title = clubReservation.Title,
                Details = clubReservation.Details,
                Link = clubReservation.EventRegisterLink,
                Status = clubReservation.Status,
                BannerPath = clubReservation.Banner
            };


            var memberships = await _repositoryManager.ClubMembership.GetAllClubMembershipsAsync(false);
            var students = memberships.Where(m => m.ClubId == club.ClubId).Select(m => m.Student);


            var notif = new Notification
            {
                CreatedAt = DateTime.Now,
                Title = $"{club.NameShortcut} klübü etkinliği",
                NotificationType = "ClupEventBildirimi",
                Message = $"{returnDto.StartTime} - {returnDto.EndTime} {newReservation.EventDate} tarihine {club.NameShortcut} klübü etkinliği bulunmaktadır! "

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


            return returnDto;
        }

        public async Task UpdateReservationAsync(int reservationId, ReservationDto reservationDto)
        {
            var reservation = await _repositoryManager.Reservation.GetReservationAsync(reservationId, trackChanges: true);

            if (reservation == null)
                throw new KeyNotFoundException($"Reservation with ID {reservationId} not found.");

            _mapper.Map(reservationDto, reservation);
            await _repositoryManager.SaveAsync();
        }

        public async Task UpdateClubReservationStatusAsync(int reservationId, string status, bool trackChanges)
        {
            var reservations = await _repositoryManager.ClubReservation.GetAllClubReservationsAsync(trackChanges);
            var reservation = reservations.Where(cr => cr.ReservationId == reservationId).FirstOrDefault();
            if (reservation is null)
                throw new ArgumentException("reservation cant be null");
            reservation.Status = status;
            await _repositoryManager.SaveAsync();
        }

        public async Task DeleteReservationAsync(int reservationId)
        {
            var reservation = await _repositoryManager.Reservation.GetReservationAsync(reservationId, trackChanges: true);

            if (reservation == null)
                throw new KeyNotFoundException($"Reservation with ID {reservationId} not found.");

            await _repositoryManager.Reservation.DeleteReservationAsync(reservation);
            await _repositoryManager.SaveAsync();
        }

        public async Task CreateLectureReservationAsync(LectureReservationCreateDto lectureReservationCreateDto)
        {
            var room = await _repositoryManager.Room.GetRoomByNameAsync(lectureReservationCreateDto.RoomName, trackChanges: false);
            if (room == null)
                throw new KeyNotFoundException($"Room '{lectureReservationCreateDto.RoomName}' not found.");
            lectureReservationCreateDto.EventDate = lectureReservationCreateDto.EventDate.Date;
            var newReservation = new Reservation
            {
                StartTime = lectureReservationCreateDto.StartTime,
                EndTime = lectureReservationCreateDto.EndTime,
                EventDate = lectureReservationCreateDto.EventDate,
                RoomId = room.RoomId
            };
            _repositoryManager.Reservation.CreateReservation(newReservation);
            await _repositoryManager.SaveAsync();
            Console.WriteLine("reservation id: ", newReservation.ReservationId);
            var lectureReservation = new LectureReservation
            {
                InstructorId = lectureReservationCreateDto.InstructorId,
                ReservationId = newReservation.ReservationId,
                Code = lectureReservationCreateDto.LectureCode
            };
            _repositoryManager.LectureReservation.CreateLectureReservation(lectureReservation);
            await _repositoryManager.SaveAsync();
            var newLectureSession = new LectureSession
            {
                StartTime = lectureReservationCreateDto.StartTime,
                EndTime = lectureReservationCreateDto.EndTime,
                LectureCode = lectureReservationCreateDto.LectureCode,
                RoomId = room.RoomId,
                InstructorId = lectureReservation.InstructorId,
                IsExtraLesson = 1,
                Date = newReservation.EventDate.Date
            };
            _repositoryManager.LectureSession.CreateLectureSession(newLectureSession);
            await _repositoryManager.SaveAsync();
            var enrollments = await _repositoryManager.Enrollment.GetAllEnrollmentsAsync(false);
            var students = enrollments.Where(e => e.LectureCode == lectureReservationCreateDto.LectureCode).Select(e => e.Student);


            var notif = new Notification
            {
                CreatedAt = DateTime.Now,
                Message = "Yeni bildiriminiz var",
                NotificationType = "EkDersBildirimi",
                Title = $"{newReservation.StartTime} - {newReservation.EndTime} {newReservation.EventDate} tarihine {lectureReservation.Code} dersi eklenmiştir! "

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

        public async Task UpdateLectureReservationAsync(int reservationId, LectureReservationUpdateDto lectureReservationUpdateDto)
        {
            var reservation = await _repositoryManager.Reservation.GetReservationAsync(reservationId, true);
            if (reservation is null)
                throw new ArgumentNullException("reservation bulunamadı");
            var room = await _repositoryManager.Room.GetRoomByNameAsync(lectureReservationUpdateDto.RoomName, false);
            if (room is null)
                throw new ArgumentNullException("room bulunamadı");
            reservation.RoomId = room.RoomId;
            reservation.StartTime = lectureReservationUpdateDto.StartTime;
            reservation.EndTime = lectureReservationUpdateDto.EndTime;
            reservation.EventDate = lectureReservationUpdateDto.EventDate;
            await _repositoryManager.SaveAsync();


        }
    }
}
