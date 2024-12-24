namespace Shared.DataTransferObjects
{
    public record NotificationRecipientDto
    (
        int Id,
        int NotificationId,
        int StudentId
    );
}
