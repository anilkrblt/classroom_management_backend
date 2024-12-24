using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassroomManagementPresentation.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    public class NotificationController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public NotificationController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: api/Notifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificationDto>>> GetNotifications()
        {
            var notifications = await _serviceManager.NotificationService.GetAllNotificationsAsync(trackChanges: false);
            return Ok(notifications);
        }

        // GET: api/Notifications/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<NotificationDto>> GetNotification(int id)
        {
            var notification = await _serviceManager.NotificationService.GetNotificationByIdAsync(id, trackChanges: false);

            if (notification == null)
                return NotFound($"Notification with ID {id} not found.");

            return Ok(notification);
        }

        // POST: api/Notifications
        [HttpPost]
        public async Task<ActionResult> CreateNotification([FromBody] NotificationDto notificationDto)
        {
            if (notificationDto == null)
                return BadRequest("NotificationDto object is null.");

            await _serviceManager.NotificationService.CreateNotificationAsync(notificationDto);
            return CreatedAtAction(nameof(GetNotification), new { id = notificationDto.NotificationId }, notificationDto);
        }

        // PUT: api/Notifications/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateNotification(int id, [FromBody] NotificationDto notificationDto)
        {
            if (notificationDto == null)
                return BadRequest("NotificationDto object is null.");

            await _serviceManager.NotificationService.UpdateNotificationAsync(id, notificationDto);
            return NoContent();
        }

        // DELETE: api/Notifications/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNotification(int id)
        {
            await _serviceManager.NotificationService.DeleteNotificationAsync(id);
            return NoContent();
        }
    }
}
