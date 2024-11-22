

using MedicalAppointment.Infraestructure.Models;
using MedicalAppointment.Infraestructure.Results;

namespace MedicalAppointment.Infraestructure.Interfaces
{
    public interface INotificationService
    {
        Task<NotificationResult> SendEmailAsync(EmailModel emailModel);
        Task<NotificationResult> SendSmsAsync(SmsModel smsModel);
        Task<NotificationResult> SendPushNotification(PushModel pushModel);

    }
}
