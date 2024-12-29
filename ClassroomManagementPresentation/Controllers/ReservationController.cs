using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Microsoft.AspNetCore.Http;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassroomManagementPresentation.Controllers
{
    [Route("api/reservations")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public ReservationController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        /*
        // GET: api/Reservations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetReservations()
        {
            var reservations = await _serviceManager.ReservationService.GetAllReservationsAsync(trackChanges: false);
            return Ok(reservations);
        }

        // GET: api/Reservations/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDto>> GetReservation(int id)
        {
            var reservation = await _serviceManager.ReservationService.GetReservationByIdAsync(id, trackChanges: false);

            if (reservation == null)
                return NotFound($"Reservation with ID {id} not found.");

            return Ok(reservation);
        }
       

        // GET: api/Reservations/ByUser/{userId}
        [HttpGet("ByUser/{userId}")]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetUserReservations(int userId)
        {
            var reservations = await _serviceManager.ReservationService.GetUserReservationsAsync(userId, trackChanges: false);

            if (reservations == null || !reservations.Any())
                return NotFound($"No reservations found for User with ID {userId}.");

            return Ok(reservations);
        }


 */
        [HttpGet("clubreservations")]
        public async Task<IEnumerable<ClubReservationGetDto>> GetAllClubReservationsAsync()
        {
            var clubReservations = await _serviceManager.ReservationService.GetAllClubReservations(false);

            return clubReservations;
        }


        // POST: api/Reservations
        [Consumes("multipart/form-data")]
        [HttpPost("clubreservation")]
        public async Task<ActionResult> CreateClubReservation([FromForm] ClubReservationPostDto dto)
        {
            // 1) Null kontrolü
            if (dto == null)
                return BadRequest("Dto is null.");

            // 2) Dosya kaydetme işlemi (isteğe bağlı “banner missing” kontrolü)
            string fileName = null;
            if (dto.BannerFile != null && dto.BannerFile.Length > 0)
            {
                // Ana proje yolunu oluştur
                var mainProjectPath = Path.GetFullPath(
                    Path.Combine(Directory.GetCurrentDirectory(), "..", "ClassroomManagement")
                );

                // Banner dosyalarının saklanacağı klasör yolu
                var bannerFolder = Path.Combine(mainProjectPath, "wwwroot", "images", "clubs", "banners");
                if (!Directory.Exists(bannerFolder))
                    Directory.CreateDirectory(bannerFolder);

                // Orijinal dosya adı ve uzantıyı alın
                var originalFileName = Path.GetFileNameWithoutExtension(dto.BannerFile.FileName);
                var fileExtension = Path.GetExtension(dto.BannerFile.FileName);

                // Benzersiz bir dosya adı oluştur (Zaman Damgası + Orijinal Ad)
                fileName = $"{originalFileName}_{DateTime.UtcNow:yyyyMMddHHmmssfff}{fileExtension}";

                // Dosya yolunu birleştir
                var savePath = Path.Combine(bannerFolder, fileName);

                // Dosyayı kaydet
                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await dto.BannerFile.CopyToAsync(stream);
                }
            }


            // 3) DTO Dönüştürme: ClubReservationPostDto -> ClubReservationDto
            // Elle map’leme örneği (AutoMapper da kullanabilirsiniz):
            var reservationDto = new ClubReservationDto
            {
                StudentId = dto.StudentId,
                ClubName = dto.ClubName,
                RoomName = dto.RoomName,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                EventTime = dto.EventTime,
                Title = dto.Title,
                Details = dto.Details,
                Link = dto.Link,
                Status = dto.Status,
                BannerPath = fileName == null
                    ? "none"
                    : Path.Combine("images", "clubs", "banners", fileName).Replace("\\", "/").Replace("//", "/").Replace(":/", "://")
            };

            // 4) Service katmanına iletme
            var createdReservation = await _serviceManager.ReservationService.CreateClubReservationAsync(reservationDto);

            // 5) Geriye 201 Created vs. dön
            // createdReservation, veritabanına eklenmiş kaydın geri dönen DTO’su olabilir
            return Ok(createdReservation);
        }



        [HttpPut("clubreservation/updatestatus/{id}")]
        public async Task<ActionResult> UpdateClubReservationStatus(int id, [FromBody] string status)
        {
            if (status == null)
                return BadRequest("status is null.");

            await _serviceManager.ReservationService.UpdateClubReservationStatusAsync(id, status, true);
            return NoContent();
        } 

/*
        [HttpPut("clubreservation/{id}")]
        public async Task<ActionResult> UpdateClubReservation(int id, [FromBody] ReservationDto reservationDto)
        {
            if (reservationDto == null)
                return BadRequest("ReservationDto object is null.");

            await _serviceManager.ReservationService.UpdateReservationAsync(id, reservationDto);
            return NoContent();
        }*/

        // DELETE: api/Reservations/{id}
        [HttpDelete("clubreservation/{ReservationId}")]
        public async Task<ActionResult> DeleteClubReservation(int ReservationId)
        {
            await _serviceManager.ReservationService.DeleteReservationAsync(ReservationId);
            return NoContent();
        } 
    }
}
