using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace ClassroomManagementPresentation.Controllers
{
    [ApiController]
    [Route("api/clubs")]
    public class ClubsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public ClubsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<ClubDto>>> GetClubs()
        {
            var reservations = await _serviceManager.ClubService.GetAllClubsAsync(false);
            return Ok(reservations);
        }
        [HttpPost("/create")]
        public async Task<ActionResult<IEnumerable<ClubDto>>> CreateClubs([FromBody] ClubDto clubDto)
        {
            var reservations = await _serviceManager.ClubService.CreateClub(clubDto, false);
            return Ok(reservations);
        }


    }
}