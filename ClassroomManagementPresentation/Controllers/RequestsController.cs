using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;



namespace backend.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequestsController : ControllerBase
    {
        /*
        private readonly ILogger<RequestsController> _logger;
        private readonly AppDbContext _context;

        public RequestsController(ILogger<RequestsController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }



        [HttpGet]
        public IActionResult GetAllRequests()
        {
            var requests = _context.Requests.ToList();
            if (requests is null)
            {
                return NotFound();
            }


            return Ok(requests);
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
