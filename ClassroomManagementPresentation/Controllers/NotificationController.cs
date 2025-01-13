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
        public async Task<ActionResult<List<NotificationDto>>> GetNotification(Guid id)
        {
            var notifications = await _serviceManager.NotificationService.GetAllNotificationsAsync(false);
            var notifs = notifications.Where(n => n.UserId == id);

            if (notifs == null)
                return NotFound($"Notification with not found.");

            return Ok(notifs);
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
    }
}
