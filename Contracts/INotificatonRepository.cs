using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetAllNotificationsAsync(bool trackChanges);
        Task<Notification> GetNotificationAsync(int notificationId, bool trackChanges);

        void CreateNotification(Notification notification);

        Task UpdateNotificationAsync(Notification notification);
        Task DeleteNotificationAsync(Notification notification);
    }
}
