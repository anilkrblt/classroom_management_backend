using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace class_management_backend.controllers
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


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetReservations()
        {
            var reservations = await _serviceManager.ReservationService.GetAllReservationsAsync(false);
            return Ok(reservations);
        }


        [HttpGet("clubreservations")]
        public async Task<IActionResult> GetAllClubReservations()
        {
            var clubReservations = await _serviceManager.ReservationService.GetAllClubReservationsAsync(false);
            return Ok(clubReservations);
        }


        [HttpGet("lecturereservations")]
        public async Task<IActionResult> GetAllLectureReservations()
        {
            var lecturereservations = await _serviceManager.ReservationService.GetAllLectureReservationsAsync(false);
            return Ok(lecturereservations);
        }
/*

        [HttpPost("createclubreservation")]
        public async Task<IActionResult> CreateclubReservation()
        {

        }


        [HttpPost("createlecturereservation")]
        public async Task<IActionResult> CreateLectureReservation()
        {
        }






        [HttpPut("updateclubreservation/{reservationId}")]
        public async Task<IActionResult> UpdateClubReservation()
        {
        }


        [HttpPut("updatelecturereservation/{reservationId}")]
        public async Task<IActionResult> UpdateLectureReservation()
        {
        }






        [HttpDelete("deletelecturereservation/{reservationId}")]

        public async Task<IActionResult> DeleteLectureReservation()
        {
        }

        [HttpDelete("deleteclubreservation/{reservationId}")]
        public async Task<IActionResult> DeleteClubReservation()
        {
        }


*/



    }

}