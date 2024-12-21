using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Contracts;
using Shared.DataTransferObjects;



namespace ClassroomManagementPresentation.Controllers
{
    [ApiController]
    [Route("api/room")]
    public class RequestsController : ControllerBase
    {

        private readonly IServiceManager _servisManager;

        public RequestsController(IServiceManager serviceManager)
        {
            _servisManager = serviceManager;
        }



        [HttpGet("/requests")]
        public IActionResult GetAllRequests()
        {
            var requests=_servisManager.RequestService.GetAllRequestsAsync(false);
            return Ok(requests);
        }

        [HttpGet("/{id:int}/request")]
        public IActionResult GetRequestsByRoomId([FromRoute(Name ="id")]int roomId)
        {
            var request=  _servisManager.RequestService.GetRequestsByRoomId(roomId,false,false);
            return Ok(request);
        }

        [HttpPut("/request/{requestId:int}/update")]
        public IActionResult UpdateRequest(int requestId,[FromBody] RequestDto requestDto)
        {
            var request=_servisManager.RequestService.UpdateRequestAsync(requestId,requestDto,true);
            return Ok(request);
        }

        [HttpPost("{roomId:int}/request/create")]
        public async Task<IActionResult>CreateRequestForInstructor([FromBody]RequestDto requestDto,[FromBody]int instructorId,int roomId)
        {
            var request=await _servisManager.RequestService.CreateRequestForRoomByInstructorIdAsync(requestDto,instructorId,roomId,true);
            return Ok(request);
        }
        [HttpPost("{roomId:int}/request/create")]
        public async Task<IActionResult>CreateRequestForStudent([FromBody]RequestDto requestDto,[FromBody]int studentId,int roomId)
        {
            var request=await _servisManager.RequestService.CreateRequestForRoomByStudentIdAsync(requestDto,studentId,roomId,true);
            return Ok(request);
        }
        [HttpDelete("{roomId:int}/request/delete")]
        public async Task<IActionResult> DeleteRequest([FromRoute(Name ="id")]int roomId,[FromBody]int requestId)
        {
            _servisManager.RequestService.DeleteRequest(roomId,requestId,true,true);
            return NoContent();
        }

        /*
        /api/request/:roomId/create-request	POST    /api/rooms/:id/create-request
        /api/request/update/:id	DELETE
        */





        


    }
}