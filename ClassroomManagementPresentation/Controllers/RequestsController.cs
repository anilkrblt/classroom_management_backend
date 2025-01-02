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

        [HttpPost()]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> CreateRequest([FromForm] RequestCreationDto dto)
        {
            if (dto == null)
                return BadRequest("Request DTO is null.");

            if (dto.PhotoFiles != null && dto.PhotoFiles.Count > 3)
            {
                return BadRequest("En fazla 3 fotoğraf yükleyebilirsiniz.");
            }

            string imagePaths = "none"; 
            if (dto.PhotoFiles != null && dto.PhotoFiles.Count > 0)
            {
                // wwwroot/images/request klasör yolunu oluşturuyoruz.
                // -> Örnek olarak: {ProjeDizin}/wwwroot/images/request
                var mainProjectPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "ClassroomManagement"));

                var requestImagesFolder = Path.Combine(mainProjectPath, "wwwroot", "images", "request");
                if (!Directory.Exists(requestImagesFolder))
                    Directory.CreateDirectory(requestImagesFolder);

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" }; // İstenilen dosya uzantıları
                var maxFileSize = 5 * 1024 * 1024; // 5 MB
                var savedFilePaths = new List<string>();

                foreach (var photo in dto.PhotoFiles.Take(3)) // Maksimum 3 fotoğraf
                {
                    if (photo.Length > 0)
                    {
                        // İmaj formatını kontrol etme
                        var fileExtension = Path.GetExtension(photo.FileName).ToLower();
                        if (!allowedExtensions.Contains(fileExtension))
                        {
                            return BadRequest($"Geçersiz dosya formatı: {fileExtension}. Sadece .jpg, .jpeg, .png ve .gif dosyalarına izin verilmektedir.");
                        }

                        if (photo.Length > maxFileSize)
                        {
                            return BadRequest($"Dosya boyutu 5 MB'ı aşıyor: {photo.FileName}");
                        }

                        var originalFileName = Path.GetFileNameWithoutExtension(photo.FileName);
                        originalFileName = originalFileName.Replace(" ", "_").Replace("-", "_");

                        var uniqueFileName = $"{originalFileName}_{DateTime.UtcNow:yyyyMMddHHmmssfff}{fileExtension}";

                        var savePath = Path.Combine(requestImagesFolder, uniqueFileName);

                        try
                        {
                            using (var stream = new FileStream(savePath, FileMode.Create))
                            {
                                await photo.CopyToAsync(stream);
                            }

                            var filePath = Path.Combine("images", "request", uniqueFileName).Replace("\\", "/");
                            savedFilePaths.Add(filePath);
                        }
                        catch (Exception ex)
                        {
                            return StatusCode(500, $"Fotoğraf kaydedilirken bir hata oluştu: {ex.Message}");
                        }
                    }
                }
                imagePaths = string.Join(",", savedFilePaths);
            }

            await _serviceManager.RequestService.CreateRequestAsync(imagePaths, dto);

            return Created();
        }



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
