using Shared.DataTransferObjects;
using Service.Contracts;
using Contracts;
using AutoMapper;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service
{
    public class NotificationService : INotificationService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public NotificationService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        // Get all notifications
        public async Task<IEnumerable<NotificationDto>> GetAllNotificationsAsync(bool trackChanges)
        {
            var notifications = await _repositoryManager.Notification.GetAllNotificationsAsync(trackChanges);
            return _mapper.Map<IEnumerable<NotificationDto>>(notifications);
        }
        public async Task<IEnumerable<NotificationDto>> GetAllNotificationsWithUserIdAsync(string userId, bool trackChanges)
        {
            var receivers = await _repositoryManager.NotificationRecipient.GetAllNotificationRecipientsAsync(trackChanges);
            var userReceivers = receivers.Where(r => r.UserId == userId).ToList();


            var notifications = userReceivers.Select(r => r.Notification);


            //var notificationDtos = _mapper.Map<List<NotificationDto>>(notifications);
            var notificationDtos = new List<NotificationDto>{};
            foreach (var a in notifications)
            {
               var x = new NotificationDto
               {
                    CreatedAt = a.CreatedAt,
                    IsRead = false,
                    Title = a.Title,
                    Message = a.Message,
                    NotificationId = a.NotificationId,
                    NotificationType= a.NotificationType

               };
               notificationDtos.Add(x);
            }

            foreach (var dto in notificationDtos)
            {
                var relatedReceivers = userReceivers.Where(r => r.NotificationId == dto.NotificationId);
                dto.IsRead = relatedReceivers.Any(r => r.IsRead);
            }

            return notificationDtos;
        }

        // Get a specific notification by ID
        public async Task<NotificationDto> GetNotificationByIdAsync(int notificationId, bool trackChanges)
        {
            var notification = await _repositoryManager.Notification.GetNotificationAsync(notificationId, trackChanges);

            if (notification == null)
                throw new KeyNotFoundException($"Notification with ID {notificationId} not found.");

            return _mapper.Map<NotificationDto>(notification);
        }

        // Create a new notification
        public async Task CreateNotificationAsync(NotificationDto notificationDto)
        {
            var notification = _mapper.Map<Notification>(notificationDto);

            _repositoryManager.Notification.CreateNotification(notification);
            await _repositoryManager.SaveAsync();
        }

        // Update an existing notification
        public async Task UpdateNotificationAsync(int notificationId, NotificationDto notificationDto)
        {
            var notification = await _repositoryManager.Notification.GetNotificationAsync(notificationId, trackChanges: true);

            if (notification == null)
                throw new KeyNotFoundException($"Notification with ID {notificationId} not found.");

            _mapper.Map(notificationDto, notification);
            await _repositoryManager.SaveAsync();
        }

        // Delete a notification
        public async Task DeleteNotificationAsync(int notificationId)
        {
            var notification = await _repositoryManager.Notification.GetNotificationAsync(notificationId, trackChanges: true);

            if (notification == null)
                throw new KeyNotFoundException($"Notification with ID {notificationId} not found.");

            await _repositoryManager.Notification.DeleteNotificationAsync(notification);
            await _repositoryManager.SaveAsync();
        }
    }
}
