using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class NotificationRecipientRepository : RepositoryBase<NotificationRecipient>, INotificationRecipientRepository
    {
        public NotificationRecipientRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<NotificationRecipient>> GetAllNotificationRecipientsAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(nr => nr.Id) 
                .ToListAsync();
        }

        public async Task<NotificationRecipient> GetNotificationRecipientAsync(int id, bool trackChanges)
        {
            return await FindByCondition(nr => nr.Id == id, trackChanges)
                .SingleOrDefaultAsync();
        }

        public void CreateNotificationRecipient(NotificationRecipient notificationRecipient)
        {
            Create(notificationRecipient);
        }

        public async Task UpdateNotificationRecipientAsync(NotificationRecipient notificationRecipient)
        {
            var existingRecipient = await GetNotificationRecipientAsync(notificationRecipient.Id, true);
            if (existingRecipient != null)
            {
                existingRecipient.NotificationId = notificationRecipient.NotificationId;
                existingRecipient.IsRead = notificationRecipient.IsRead;
                existingRecipient.ReadAt = DateTime.Now;

                Update(existingRecipient);
            }
        }

        // Delete a notification recipient
        public async Task DeleteNotificationRecipientAsync(NotificationRecipient notificationRecipient)
        {
            var existingRecipient = await GetNotificationRecipientAsync(notificationRecipient.Id, true);
            if (existingRecipient != null)
            {
                Delete(existingRecipient);
            }
        }
    }
}
