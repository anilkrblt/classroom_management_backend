using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
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
        [HttpGet("clubreservations")]
        public async Task<IEnumerable<ClubReservationGetDto>> GetAllClubReservationsAsync()
        {
            return await _serviceManager.ReservationService.GetAllClubReservations(false);


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

        // POST: api/Reservations
        [HttpPost]
        public async Task<ActionResult> CreateReservation([FromBody] ReservationDto reservationDto)
        {
            if (reservationDto == null)
                return BadRequest("ReservationDto object is null.");

            await _serviceManager.ReservationService.CreateReservationAsync(reservationDto);
            return CreatedAtAction(nameof(GetReservation), new { id = reservationDto.ReservationId }, reservationDto);
        }

        // PUT: api/Reservations/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateReservation(int id, [FromBody] ReservationDto reservationDto)
        {
            if (reservationDto == null)
                return BadRequest("ReservationDto object is null.");

            await _serviceManager.ReservationService.UpdateReservationAsync(id, reservationDto);
            return NoContent();
        }

        // DELETE: api/Reservations/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReservation(int id)
        {
            await _serviceManager.ReservationService.DeleteReservationAsync(id);
            return NoContent();
        }
    }
}
