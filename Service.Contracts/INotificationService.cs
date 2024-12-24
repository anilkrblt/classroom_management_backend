using Shared.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface INotificationService
    {
        // Get all notifications
        Task<IEnumerable<NotificationDto>> GetAllNotificationsAsync(bool trackChanges);

        // Get a specific notification by ID
        Task<NotificationDto> GetNotificationByIdAsync(int notificationId, bool trackChanges);

        // Create a new notification
        Task CreateNotificationAsync(NotificationDto notificationDto);

        // Update an existing notification
        Task UpdateNotificationAsync(int notificationId, NotificationDto notificationDto);

        // Delete a notification
        Task DeleteNotificationAsync(int notificationId);
    }
}
