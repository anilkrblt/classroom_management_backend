using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class NotificationRepository : RepositoryBase<Notification>, INotificationRepository
    {
        public NotificationRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Notification>> GetAllNotificationsAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(n => n.CreatedAt) 
                .ToListAsync();
        }

        public async Task<Notification> GetNotificationAsync(int notificationId, bool trackChanges)
        {
            return await FindByCondition(n => n.NotificationId == notificationId, trackChanges)
                .SingleOrDefaultAsync();
        }

        public void CreateNotification(Notification notification)
        {
            Create(notification);
        
        }

        public async Task UpdateNotificationAsync(Notification notification)
        {
            var existingNotification = await GetNotificationAsync(notification.NotificationId, true);
            if (existingNotification != null)
            {
                existingNotification.Message = notification.Message;
                existingNotification.CreatedAt = notification.CreatedAt;

                Update(existingNotification);
            }
        }

        public async Task DeleteNotificationAsync(Notification notification)
        {
            var existingNotification = await GetNotificationAsync(notification.NotificationId, true);
            if (existingNotification != null)
            {
                Delete(existingNotification);
            }
        }
    }
}
