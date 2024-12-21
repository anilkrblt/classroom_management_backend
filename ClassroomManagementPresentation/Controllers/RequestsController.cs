using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Contracts;



namespace ClassroomManagementPresentation.Controllers
{
    [ApiController]
    [Route("api/requests")]
    public class RequestsController : ControllerBase
    {

        private readonly IServiceManager _servisManager;

        public RequestsController(ILogger<RequestsController> logger, IServiceManager serviceManager)
        {
            _servisManager = serviceManager;
        }



        [HttpGet()]
        public IActionResult GetAllRequests()
        {
            var requests=_servisManager.RequestService.GetAllRequestsAsync(false);
            return Ok(requests);
        }

        [HttpGet("request/{id:int}")]
        public async Task<IActionResult> GetRequestsByRoomId([FromRoute(Name ="id")]int roomId)
        {
            var request=await _servisManager.RequestService.GetRequestsByRoomId(roomId,false,false);
            return Ok(request);
        }

        /*
        Route	Method
        /api/requets	GET
        /api/requets/:roomId	GET   /api/rooms/:id/requests daha mantıklı oluyor
        /api/request/update/:id	PUT   
        /api/request/:roomId/create-request	POST    /api/rooms/:id/create-request
        /api/request/update/:id	DELETE
        */





        


    }
}