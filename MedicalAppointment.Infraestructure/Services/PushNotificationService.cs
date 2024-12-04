using MedicalAppointment.Infraestructure.Interfaces;
using MedicalAppointment.Infraestructure.Models;
using MedicalAppointment.Infraestructure.Results;

namespace BoletosApp.Infraestructure.Services
{
    public class PushNotificationService : INotificationService
    {
        public Task<NotificationResult> SendEmailAsync(EmailModel emailModel)
        {
            throw new NotImplementedException();
        }

        public Task<NotificationResult> SendPushNotification(PushModel pushModel)
        {
            throw new NotImplementedException();
        }

        public Task<NotificationResult> SendSmsAsync(SmsModel smsModel)
        {
            throw new NotImplementedException();
        }
    }
}