using System;
using System.Collections.Generic;

namespace Shared.DataTransferObjects
{
    public record NotificationDto
    (
        int NotificationId,
        string Message,
        DateTime CreatedAt,
        IEnumerable<NotificationRecipientDto> NotificationRecipients
    );
}
