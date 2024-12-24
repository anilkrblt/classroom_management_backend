using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface INotificationRecipientRepository
    {
        Task<IEnumerable<NotificationRecipient>> GetAllNotificationRecipientsAsync(bool trackChanges);
        Task<NotificationRecipient> GetNotificationRecipientAsync(int id, bool trackChanges);

        void CreateNotificationRecipient(NotificationRecipient notificationRecipient);

        Task UpdateNotificationRecipientAsync(NotificationRecipient notificationRecipient);
        Task DeleteNotificationRecipientAsync(NotificationRecipient notificationRecipient);
    }
}
