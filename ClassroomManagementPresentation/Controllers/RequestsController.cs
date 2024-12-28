using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassroomManagementPresentation.Controllers
{
    [ApiController]
    [Route("api/complaints")]
    public class RequestsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public RequestsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestDto>>> GetRequests()
        {
            var requests = await _serviceManager.RequestService.GetAllRequestsAsync(trackChanges: false);
            return Ok(requests);
        }

        // GET: api/Requests/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<RequestDto>> GetRequest(int id)
        {
            var request = await _serviceManager.RequestService.GetRequestByIdAsync(id, trackChanges: false);

            if (request == null)
                return NotFound($"Request with ID {id} not found.");

            return Ok(request);
        }

        // GET: api/Requests/ByRoom/{roomId}
        [HttpGet("ByRoom/{roomId}")]
        public async Task<ActionResult<IEnumerable<RequestDto>>> GetRoomRequests(int roomId)
        {
            var requests = await _serviceManager.RequestService.GetRequestsByRoomIdAsync(roomId, trackChanges: false);

            if (requests == null || !requests.Any())
                return NotFound($"No requests found for Room with ID {roomId}.");

            return Ok(requests);
        }

        // GET: api/Requests/ByUser/{userId}
        [HttpGet("ByUser/{userId}")]
        public async Task<ActionResult<IEnumerable<RequestDto>>> GetUserRequests(int userId)
        {
            var requests = await _serviceManager.RequestService.GetRequestsByUserIdAsync(userId, trackChanges: false);

            if (requests == null || !requests.Any())
                return NotFound($"No requests found for User with ID {userId}.");

            return Ok(requests);
        }
/*
        // POST: api/Requests
        [HttpPost]
        public async Task<ActionResult> CreateRequest([FromBody] RequestDto requestDto)
        {
            if (requestDto == null)
                return BadRequest("RequestDto object is null.");

            await _serviceManager.RequestService.CreateRequestAsync(requestDto);
            return CreatedAtAction(nameof(GetRequest), new { id = requestDto.RequestId }, requestDto);
        }
        [HttpPost("upload")]
        public async Task<IActionResult> UploadPhoto(
    [FromForm] IFormFile photo,
    [FromForm] string title,
    [FromForm] string description)
        {
            if (photo == null || photo.Length == 0)
                return BadRequest("Fotoğraf yüklenmedi.");

            // 1) Dosya adı / path
            var fileName = Path.GetFileName(photo.FileName);
            // Örneğin: "resim.jpg"

            // 2) Hedef klasör
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            // 3) Tam yol
            var filePath = Path.Combine(uploadPath, fileName);

            // 4) Dosyayı kaydet
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await photo.CopyToAsync(stream);
            }

            // 5) Veritabanına (dosya adı / yol + diğer alanlar) kaydedebilirsiniz
            //    Örnek bir tablo: Photos (Id, FileName, Title, Description, CreatedAt ...)
            // photo kaydedildiyse, DB'ye fileName, title, description vs. eklenebilir
            // Aşağıda basit bir örnek akış:
            // 
            // var entity = new PhotoEntity 
            // {
            //     FileName = fileName,
            //     Title = title,
            //     Description = description,
            //     CreatedAt = DateTime.Now
            // };
            // _context.Photos.Add(entity);
            // await _context.SaveChangesAsync();

            return Ok(new { Message = "Fotoğraf yüklendi.", FileName = fileName, Title = title, Description = description });
        }
*/

        // PUT: api/Requests/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRequest(int id, [FromBody] RequestDto requestDto)
        {
            if (requestDto == null)
                return BadRequest("RequestDto object is null.");

            await _serviceManager.RequestService.UpdateRequestAsync(id, requestDto);
            return NoContent();
        }

        // DELETE: api/Requests/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRequest(int id)
        {
            await _serviceManager.RequestService.DeleteRequestAsync(id);
            return NoContent();
        }
    }
}
