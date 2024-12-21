using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using backend.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace backend.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        /*
        private readonly ILogger<RoomsController> _logger;

        
        private readonly AppDbContext _context;

        public RoomsController(ILogger<RoomsController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // bu action room controller altında olmalı
        [HttpGet]
        public IActionResult GetRequestsByRoomId([FromBody] int id)
        {

            var requests = _context.Requests.Where(req => req.RoomId == id).ToList();

            if (requests is null)
            {
                return NotFound();
            }
            return Ok(requests);

        }
        
        Route	Method
        /api/requets	GET
        /api/requets/:roomId	GET   /api/rooms/:id/requests daha mantıklı oluyor
        /api/request/update/:id	PUT                  /api/rooms/:id/update-request
        /api/request/:roomId/create-request	POST    /api/rooms/:id/create-request
        /api/request/update/:id	DELETE
     
        [Route("{id}/create-request")]
        [HttpPost]
        public IActionResult CreateRequest([FromBody]Request request){
            if (request is null){
                return BadRequest();
            }
            if (ModelState.IsValid){
                return BadRequest();
            }
            _context.Requests.Add(request);
            _context.SaveChanges();
            return Ok(request);
        }
        [Route("{id}/update-request")]
        [HttpPut("{id:int}")]
        public IActionResult UpdateRequest([FromRoute]int id, [FromBody]Request Request){
            var room = _context.Rooms.FirstOrDefault(room => room.Id == id);
            if(room is null){
                return NotFound();
            }
           var request = room.Requests.FirstOrDefault(req => req.Id == Request.Id);
           request = Request;
        
           return Ok(request);

        }

        [Route("{RoomId}/delete-request")]
        [HttpPut("{RoomId}")]
        public IActionResult DeleteRequest([FromRoute]int RoomId, [FromBody]int reqId){
            var room = _context.Rooms.FirstOrDefault(room => room.Id == RoomId);
            if(room is null){
                return NotFound();
            }
           var request = room.Requests.FirstOrDefault(req => req.Id == reqId);
          
           return Ok(request);

        }
        */


    }
}