using System;
using AutoMapper;
using backend.controllers;
using backend.Dto;
using backend.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace class_management_backend.controllers
{
    [Route("api/reservations")]
    [ApiController]

    public class ReservationController : ControllerBase
    {
        
        private readonly AppDbContext context;
        private readonly IMapper mapper;
        public ReservationController(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetReservations()
        {
            var reservations = await context.Reservations
                .Include(r => r.Room)
                .Include(r => r.Instructor)  // ApprovedBy yerine Instructor'ı include et
                .Include(r => r.LectureReservations)
                .Include(r => r.ClubReservations)
                .ToListAsync();

            var reservationsDTO = reservations.Select(r => new ReservationDto
            {
                Id = r.ReservationId,
                EventDate = r.EventDate,
                StartTime = r.StartTime.ToString("HH:mm"), // Saat formatı ekleyin
                EndTime = r.EndTime.ToString("HH:mm"), // Saat formatı ekleyin
                Description = r.Description,
                RoomId = r.RoomId,
                Approver = r.Instructor != null ? new ApproverDto
                {
                    InstructorId = r.Instructor.InstructorId,
                    Instructor = r.Instructor.Name // Null kontrolü burada
                } : null,  // Eğer Instructor null ise null döndürecektir
                           // LectureReservations ve ClubReservations için ilişkilendirilmiş detayları döndürme
                LectureReservations = r.LectureReservations.Select(lr => new LectureReservationDto
                {
                    ReservationId = lr.ReservationId,
                    InstructorId = lr.InstructorId,
                    Reservation = lr.Reservation != null ? lr.Reservation.ToString() : "No Reservation" // Null kontrolü ekle
                }).ToList(),
                ClubReservations = r.ClubReservations.Select(cr => new ClubReservationDto
                {
                    ReservationId = cr.ReservationId,
                    ClubId = cr.ClubId,
                    Reservation = cr.Reservation != null ? cr.Reservation.ToString() : "No Reservation" // Null kontrolü ekle
                }).ToList()
            }).ToList();

            return Ok(reservationsDTO);
        }


        [HttpGet("clubreservations")]
        public async Task<IActionResult> GetAllClubReservations()
        {
            try
            {
                var clubReservations = await context.ClubReservations
                    .Include(cr => cr.Reservation)  // Reservation ile ilişkilendir
                        .ThenInclude(r => r.Instructor)  // Instructor'ı dahil et
                    .Include(cr => cr.Club)  // Club'ı dahil et
                        .ThenInclude(c => c.Student)   // Club'ın Student'ını dahil et (kulüp yöneticisi)
                    .Select(cr => new
                    {
                        cr.ReservationId,
                        EventDate = cr.Reservation.EventDate,
                        StartTime = cr.Reservation.StartTime.ToString("HH:mm"),  // Saat formatını ekledim
                        EndTime = cr.Reservation.EndTime.ToString("HH:mm"),    // Saat formatını ekledim
                        Description = cr.Reservation.Description,
                        RoomName = cr.Reservation.Room.Name,
                        ApprovedBy = cr.Reservation.Instructor != null
                            ? cr.Reservation.Instructor.Name
                            : "No Approver",  // Null kontrolü ekledim
                        ClubName = cr.Club.Name,
                        ClubDirector = cr.Club.Student != null
                            ? cr.Club.Student.Name   // Kulüp yöneticisinin adı
                            : "No Director"  // Null kontrolü ekledim
                    })
                    .ToListAsync();

                if (!clubReservations.Any())
                {
                    return NotFound(new { message = "No club reservations found." });
                }

                return Ok(clubReservations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching club reservations.", error = ex.Message });
            }
        }


        [HttpGet("lecturereservations")]
        public async Task<IActionResult> GetAllLectureReservations()
        {
            try
            {
                var lectureReservations = await context.LectureReservations
                    .Include(lr => lr.Reservation)  // Reservation bilgisini dahil et
                        .ThenInclude(r => r.Instructor)  // Instructor'ı dahil et
                    .Include(lr => lr.Reservation.Room)  // Room bilgisini dahil et
                    .Select(lr => new
                    {
                        lr.ReservationId,
                        EventDate = lr.Reservation.EventDate,
                        StartTime = lr.Reservation.StartTime.ToString("HH:mm"),  // Saat formatını ekledim
                        EndTime = lr.Reservation.EndTime.ToString("HH:mm"),    // Saat formatını ekledim
                        Description = lr.Reservation.Description,
                        RoomName = lr.Reservation.Room.Name,
                        ApprovedBy = lr.Reservation.Instructor != null
                            ? lr.Reservation.Instructor.Name
                            : "No Approver",  // Null kontrolü ekledim
                        InstructorName = lr.Instructor != null
                            ? lr.Instructor.Name
                            : "No Instructor",  // Null kontrolü ekledim
                        InstructorTitle = lr.Instructor != null
                            ? lr.Instructor.Title
                            : "No Title"  // Null kontrolü ekledim
                    })
                    .ToListAsync();

                if (!lectureReservations.Any())
                {
                    return NotFound(new { message = "No lecture reservations found." });
                }

                return Ok(lectureReservations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching lecture reservations.", error = ex.Message });
            }
        }







        [HttpPost("createclubreservation")]
        public async Task<IActionResult> CreateclubReservation([FromBody] ClubReservationRequest request)
        {
            try
            {
                // Oda bilgisini bul
                var room = await context.Rooms.FirstOrDefaultAsync(r => r.Name == request.RoomName);
                if (room == null)
                    return NotFound(new { message = "Room not found." });

                // EventDate zaten DateTime olarak geldiği için, buraya herhangi bir parse işlemi gerekmez
                DateTime eventDate = DateTime.Parse(request.EventDate);

                // Kulüp bilgisi ve direktörü almak
                var club = await context.Clubs
                    .Include(c => c.Student) // Student'ı dahil ediyoruz, çünkü Director öğrencidir
                    .FirstOrDefaultAsync(c => c.Name == request.ClubName);

                if (club == null)
                    return NotFound(new { message = "Club not found." });

                // Ensure the student linked to the club exists (Director)
                if (club.Student == null)
                    return NotFound(new { message = "Club's director (Student) not found." });

                // Aynı oda, tarih ve saat diliminde başka bir rezervasyon olup olmadığını kontrol et
                var existingReservations = await context.Reservations
                    .Where(r => r.RoomId == room.RoomId && r.EventDate == eventDate)
                    .ToListAsync(); // Verileri belleğe al

                // Convert request StartTime and EndTime to DateTime
                TimeSpan startTime = TimeSpan.Parse(request.StartTime); // Example: "13:30:00"
                TimeSpan endTime = TimeSpan.Parse(request.EndTime); // Example: "14:30:00"

                DateTime startDateTime = eventDate.Add(startTime); // Combine EventDate with StartTime
                DateTime endDateTime = eventDate.Add(endTime); // Combine EventDate with EndTime

                // Check if there's a time conflict
                bool isConflict = existingReservations.Any(r =>
                    (startDateTime < r.EndTime && endDateTime > r.StartTime)); // Compare DateTime with DateTime

                if (isConflict)
                {
                    return BadRequest(new { message = "The room is already reserved for the specified time." });
                }

                // Ensure the Instructor exists (to satisfy ApprovedBy foreign key)
                var instructor = await context.Instructors.FirstOrDefaultAsync(); // Or get the instructor from the logged-in user or other source
                if (instructor == null)
                    return BadRequest(new { message = "Instructor for approval not found." });

                // Reservation nesnesi oluştur ve kaydet
                var reservation = new Reservation
                {
                    EventDate = eventDate,
                    StartTime = startDateTime,
                    EndTime = endDateTime,
                    Description = request.Description,
                    RoomId = room.RoomId,
                    ApprovedBy = instructor.InstructorId, // Ensure the Instructor's PersonId is used
                    LectureReservations = new List<LectureReservation>(),
                    ClubReservations = new List<ClubReservation>()
                };

                // Add reservation to the context and save
                context.Reservations.Add(reservation);
                await context.SaveChangesAsync();

                // ClubReservation nesnesi oluştur ve kaydet
                var clubReservation = new ClubReservation
                {
                    ReservationId = reservation.ReservationId, // Reservation must exist now
                    ClubId = club.ClubId, // Ensure ClubId is valid
                    Club = club, // The full Club object
                };

                // Add club reservation to the context and save
                context.ClubReservations.Add(clubReservation);
                await context.SaveChangesAsync();

                // Yanıt olarak dönecek DTO
                var response = new
                {
                    EventDate = reservation.EventDate.ToString("yyyy-MM-dd"),
                    StartTime = reservation.StartTime.ToString(@"hh\:mm\:ss"),
                    EndTime = reservation.EndTime.ToString(@"hh\:mm\:ss"),
                    Description = reservation.Description,
                    RoomName = room.Name,
                    ClubName = club.Name,
                    ClubDirector = club.Student?.Name // Director bilgisi Student tablosunda
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the club reservation.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }


        [HttpPost("createlecturereservation")]
        public async Task<IActionResult> CreateLectureReservation([FromBody] LectureReservationRequest request)
        {
            try
            {
                // Oda bilgisini al
                var room = await context.Rooms.FirstOrDefaultAsync(r => r.Name == request.RoomName);
                if (room == null)
                    return NotFound(new { message = "Room not found." });

                // Tarih ve saat bilgilerini işle
                DateTime eventDate = DateTime.Parse(request.EventDate);
                TimeSpan startTime = TimeSpan.Parse(request.StartTime);
                TimeSpan endTime = TimeSpan.Parse(request.EndTime);
                DateTime startDateTime = eventDate.Add(startTime);
                DateTime endDateTime = eventDate.Add(endTime);

                // Çakışma kontrolü
                var existingReservations = await context.Reservations
                    .Where(r => r.RoomId == room.RoomId && r.EventDate.Date == eventDate.Date)
                    .ToListAsync();

                bool isConflict = existingReservations.Any(r =>
                    startDateTime < r.EndTime && endDateTime > r.StartTime);

                if (isConflict)
                {
                    return BadRequest(new { message = "The room is already reserved for the specified time." });
                }

                // Eğitmen bilgilerini kontrol et
                var instructor = await context.Instructors
                    .Include(i => i.LectureSessions)
                    .FirstOrDefaultAsync(i => i.Name == request.InstructorName && i.Title == request.InstructorTitle);
                if (instructor == null)
                    return NotFound(new { message = "Instructor not found." });

                // Lecture bilgisini kontrol et
                var lecture = await context.Lectures
                    .FirstOrDefaultAsync(l => l.Code == request.LectureCode);
                if (lecture == null)
                {
                    return NotFound(new { message = "Lecture not found." });
                }

                // Yeni rezervasyon oluştur
                var reservation = new Reservation
                {
                    EventDate = eventDate,
                    StartTime = startDateTime,
                    EndTime = endDateTime,
                    Description = request.Description,
                    RoomId = room.RoomId,
                    ApprovedBy = instructor.InstructorId,
                    LectureReservations = new List<LectureReservation>(),
                    ClubReservations = new List<ClubReservation>()
                };
                context.Reservations.Add(reservation);
                await context.SaveChangesAsync();

                // LectureSession oluştur
                var lectureSession = new LectureSession
                {
                    StartTime = startDateTime,
                    EndTime = endDateTime,
                    LectureCode = lecture.Code,
                    Lecture = lecture,
                    InstructorId = instructor.InstructorId,
                    Instructor = instructor,
                    RoomId = room.RoomId,
                    Room = room
                };
                context.LectureSessions.Add(lectureSession);
                await context.SaveChangesAsync();

                // LectureReservation oluştur
                var lectureReservation = new LectureReservation
                {
                    ReservationId = reservation.ReservationId,
                    InstructorId = instructor.InstructorId,
                    Instructor = instructor
                };
                context.LectureReservations.Add(lectureReservation);
                await context.SaveChangesAsync();

                // Yanıt oluştur
                var response = new
                {
                    EventDate = reservation.EventDate.ToString("yyyy-MM-dd"),
                    StartTime = reservation.StartTime.ToString(@"hh\:mm"),
                    EndTime = reservation.EndTime.ToString(@"hh\:mm"),
                    Description = reservation.Description,
                    RoomName = room.Name,
                    InstructorName = instructor.Name,
                    InstructorTitle = instructor.Title,
                    LectureCode = lectureSession.LectureCode,
                    LectureName = lecture.Name
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the lecture reservation.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }






        [HttpPut("updateclubreservation/{reservationId}")]
        public async Task<IActionResult> UpdateClubReservation(int reservationId, [FromBody] ClubReservationRequest request)
        {
            try
            {
                // Rezervasyonu bul
                var reservation = await context.ClubReservations
                    .Include(cr => cr.Reservation) // Reservation'ı dahil et
                    .ThenInclude(r => r.Room)
                    .Include(cr => cr.Club)
                    .ThenInclude(c => c.Student) // Club ve student bilgilerini dahil et
                    .FirstOrDefaultAsync(cr => cr.ReservationId == reservationId);

                if (reservation == null)
                {
                    return NotFound(new { message = "Club reservation not found." });
                }

                // Oda bilgisini bul
                var room = await context.Rooms.FirstOrDefaultAsync(r => r.Name == request.RoomName);
                if (room == null)
                {
                    return NotFound(new { message = "Room not found." });
                }

                // Kulüp bilgisini bul
                var club = await context.Clubs
                    .Include(c => c.Student) // Öğrenci bilgisi (direktör) dahil ediliyor
                    .FirstOrDefaultAsync(c => c.Name == request.ClubName);

                if (club == null)
                {
                    return NotFound(new { message = "Club not found." });
                }

                // EventDate zaten DateTime olarak geldiği için buraya herhangi bir parse işlemi gerekmez
                DateTime eventDate = DateTime.Parse(request.EventDate);

                // Convert request StartTime and EndTime to DateTime
                TimeSpan startTime = TimeSpan.Parse(request.StartTime); // Örneğin: "13:30:00"
                TimeSpan endTime = TimeSpan.Parse(request.EndTime); // Örneğin: "14:30:00"

                DateTime startDateTime = eventDate.Add(startTime); // EventDate ile StartTime'ı birleştir
                DateTime endDateTime = eventDate.Add(endTime); // EventDate ile EndTime'ı birleştir

                // Aynı oda, tarih ve saat diliminde başka bir rezervasyon olup olmadığını kontrol et
                var existingReservations = await context.Reservations
                    .Where(r => r.RoomId == room.RoomId && r.EventDate == eventDate && r.ReservationId != reservation.ReservationId)
                    .ToListAsync(); // Verileri belleğe al

                bool isConflict = existingReservations.Any(r =>
                    (startDateTime < r.EndTime && endDateTime > r.StartTime)); // Zaman çakışmasını kontrol et

                if (isConflict)
                {
                    return BadRequest(new { message = "The room is already reserved for the specified time." });
                }

                // Sadece ClubReservation'ı güncelle
                reservation.Reservation.EventDate = eventDate;
                reservation.Reservation.StartTime = startDateTime;
                reservation.Reservation.EndTime = endDateTime;
                reservation.Reservation.Description = request.Description;
                reservation.Reservation.RoomId = room.RoomId; // Oda bilgisi güncelleniyor

                reservation.ClubId = club.ClubId; // Kulüp bilgisi güncelleniyor

                // Veritabanına kaydet
                await context.SaveChangesAsync();

                // Yanıt olarak dönecek DTO
                var response = new
                {
                    EventDate = reservation.Reservation.EventDate.ToString("yyyy-MM-dd"),
                    StartTime = reservation.Reservation.StartTime.ToString(@"hh\:mm\:ss"),
                    EndTime = reservation.Reservation.EndTime.ToString(@"hh\:mm\:ss"),
                    Description = reservation.Reservation.Description,
                    RoomName = room.Name,
                    ClubName = club.Name,
                    ClubDirector = club.Student?.Name // Direktör bilgisi (öğrenci)
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the club reservation.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }


        [HttpPut("updatelecturereservation/{reservationId}")]
        public async Task<IActionResult> UpdateLectureReservation(int reservationId, [FromBody] LectureReservationRequest request)
        {
            try
            {
                // Mevcut rezervasyonu al
                var lectureReservation = await context.LectureReservations
                    .Include(lr => lr.Reservation)
                    .ThenInclude(r => r.Room)
                    .Include(lr => lr.Instructor)
                    .FirstOrDefaultAsync(lr => lr.ReservationId == reservationId);

                if (lectureReservation == null)
                    return NotFound(new { message = "Lecture reservation not found." });

                var reservation = lectureReservation.Reservation;

                // Oda bilgisini al
                var room = await context.Rooms.FirstOrDefaultAsync(r => r.Name == request.RoomName);
                if (room == null)
                    return NotFound(new { message = "Room not found." });

                // Tarih ve saat bilgilerini işle
                DateTime eventDate = DateTime.Parse(request.EventDate);
                TimeSpan startTime = TimeSpan.Parse(request.StartTime);
                TimeSpan endTime = TimeSpan.Parse(request.EndTime);
                DateTime startDateTime = eventDate.Add(startTime);
                DateTime endDateTime = eventDate.Add(endTime);

                // Çakışma kontrolü
                var existingReservations = await context.Reservations
                    .Where(r => r.RoomId == room.RoomId && r.EventDate.Date == eventDate.Date && r.ReservationId != reservation.ReservationId)
                    .ToListAsync();

                bool isConflict = existingReservations.Any(r =>
                    startDateTime < r.EndTime && endDateTime > r.StartTime);

                if (isConflict)
                {
                    return BadRequest(new { message = "The room is already reserved for the specified time." });
                }

                // Eğitmen bilgisini kontrol et
                var instructor = await context.Instructors
                    .Include(i => i.LectureSessions)
                    .FirstOrDefaultAsync(i => i.Name == request.InstructorName && i.Title == request.InstructorTitle);
                if (instructor == null)
                    return NotFound(new { message = "Instructor not found." });

                // Lecture bilgisi kontrol et
                var lecture = await context.Lectures
                    .FirstOrDefaultAsync(l => l.Code == request.LectureCode);
                if (lecture == null)
                {
                    return NotFound(new { message = "Lecture not found." });
                }

                // Rezervasyon ve oturum bilgilerini güncelle
                reservation.EventDate = eventDate;
                reservation.StartTime = startDateTime;
                reservation.EndTime = endDateTime;
                reservation.Description = request.Description;
                reservation.RoomId = room.RoomId;

                // Instructor'ın LectureSession ilişkisini kontrol et
                var lectureSession = await context.LectureSessions
                    .FirstOrDefaultAsync(ls => ls.LectureCode == lecture.Code && ls.InstructorId == instructor.InstructorId && ls.StartTime == startDateTime);

                if (lectureSession != null)
                {
                    // Eğer mevcut bir LectureSession varsa, onu güncelle
                    lectureSession.StartTime = startDateTime;
                    lectureSession.EndTime = endDateTime;
                    lectureSession.LectureCode = lecture.Code;
                    lectureSession.Lecture = lecture;
                    lectureSession.InstructorId = instructor.InstructorId;
                    lectureSession.Instructor = instructor;
                    lectureSession.RoomId = room.RoomId;
                    lectureSession.Room = room;
                }
                else
                {
                    // Yeni bir LectureSession oluştur
                    lectureSession = new LectureSession
                    {
                        StartTime = startDateTime,
                        EndTime = endDateTime,
                        LectureCode = lecture.Code,
                        Lecture = lecture,
                        InstructorId = instructor.InstructorId,
                        Instructor = instructor,
                        RoomId = room.RoomId,
                        Room = room
                    };
                    context.LectureSessions.Add(lectureSession);
                }

                // Veritabanına kaydet
                await context.SaveChangesAsync();

                // Yanıt oluştur
                var response = new
                {
                    EventDate = reservation.EventDate.ToString("yyyy-MM-dd"),
                    StartTime = reservation.StartTime.ToString(@"hh\:mm"),
                    EndTime = reservation.EndTime.ToString(@"hh\:mm"),
                    Description = reservation.Description,
                    RoomName = room.Name,
                    InstructorName = instructor.Name,
                    InstructorTitle = instructor.Title,
                    LectureCode = lectureSession.LectureCode,
                    LectureName = lecture.Name
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the lecture reservation.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }






        [HttpDelete("deletelecturereservation/{reservationId}")]
       
        public async Task<IActionResult> DeleteLectureReservation(int reservationId)
        {
            try
            {
                // LectureReservation'ı ve ilişkili verileri dahil ederek al
                var lectureReservation = await context.LectureReservations
                    .Include(lr => lr.Reservation) // Reservation dahil et
                    .Include(lr => lr.Instructor)  // Instructor dahil et
                    .FirstOrDefaultAsync(lr => lr.ReservationId == reservationId);

                if (lectureReservation == null)
                {
                    return NotFound(new { message = "Lecture reservation not found." });
                }

                // Instructor üzerinden LectureSession'lara eriş
                var instructor = lectureReservation.Instructor;

                // Instructor'ın LectureSession'larını al
                var lectureSessions = await context.LectureSessions
                    .Where(ls => ls.InstructorId == instructor.InstructorId)
                    .ToListAsync();

                // Her bir LectureSession'ı sil
                foreach (var session in lectureSessions)
                {
                    context.LectureSessions.Remove(session);
                    Console.WriteLine($"Removed LectureSession with ID: {session.LectureSessionId}");
                }

                // LectureReservation'ı sil
                context.LectureReservations.Remove(lectureReservation);
                Console.WriteLine($"Removed LectureReservation with ID: {lectureReservation.ReservationId}");

                // Reservation'ı sil
                context.Reservations.Remove(lectureReservation.Reservation);
                Console.WriteLine($"Removed Reservation with ID: {lectureReservation.ReservationId}");

                // Veritabanına kaydet
                await context.SaveChangesAsync();

                return Ok(new { message = "Lecture reservation and related data have been successfully deleted." });
            }
            catch (Exception ex)
            {
                // Hata durumunda daha fazla bilgi verelim
                Console.WriteLine($"Error details: {ex.Message}");
                return StatusCode(500, new { message = "An error occurred while deleting the lecture reservation.", error = ex.Message });
            }
        }

        [HttpDelete("deleteclubreservation/{reservationId}")]
        public async Task<IActionResult> DeleteClubReservation(int reservationId)
        {
            try
            {
                // ClubReservation'ı ve ilişkili verileri dahil ederek al
                var clubReservation = await context.ClubReservations
                    .Include(cr => cr.Reservation) // Reservation dahil et
                    .FirstOrDefaultAsync(cr => cr.ReservationId == reservationId);

                if (clubReservation == null)
                {
                    return NotFound(new { message = "Club reservation not found." });
                }

                // ClubReservation'ı sil
                context.ClubReservations.Remove(clubReservation);
                Console.WriteLine($"Removed ClubReservation with ID: {clubReservation.ReservationId}");

                // Reservation'ı sil
                context.Reservations.Remove(clubReservation.Reservation);
                Console.WriteLine($"Removed Reservation with ID: {clubReservation.ReservationId}");

                // Veritabanına kaydet
                await context.SaveChangesAsync();

                return Ok(new { message = "Club reservation and related data have been successfully deleted." });
            }
            catch (Exception ex)
            {
                // Hata durumunda daha fazla bilgi verelim
                Console.WriteLine($"Error details: {ex.Message}");
                return StatusCode(500, new { message = "An error occurred while deleting the club reservation.", error = ex.Message });
            }
        }






    }

}