using MedicalAppointment.Persistance.Base;
using MedicalAppointment.Persistance.Context;
using MedicalAppointment.Persistance.Interfaces.System;
using MedicalAppointmentApp.Domain.Entities.System;
using MedicalAppointmentApp.Domain.Result;
using Microsoft.Extensions.Logging;

namespace MedicalAppointment.Persistance.Repositories.System
{
    public class NotificationRepository(MedicalAppointmentContext medicalAppointmentContext, ILogger<NotificationRepository> logger)
        : BaseRepository<Notification>(medicalAppointmentContext), INotificationRepository
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private readonly ILogger<NotificationRepository> logger;

        private async Task<OperationResult> ExecuteOperationWithLogging(Func<Task<OperationResult>> operation, string errorMessage)
        {
            var operationResult = new OperationResult();
            try
            {
                return await operation();
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = errorMessage;
                logger.LogError(ex, errorMessage);
                return operationResult;
            }
        }

        public async Task<OperationResult> ScheduleReminder(int userId, string message, DateTime reminderDate)
        {
            return await ExecuteOperationWithLogging(async () =>
            {
                var notification = new Notification
                {
                    UserID = userId,
                    Message = message,
                    SentAt = reminderDate
                };

                await base.Save(notification);
                SimulateEmailSending(userId, message);

                return new OperationResult
                {
                    Success = true,
                    Message = "Recordatorio para cita programado con éxito."
                };
            }, "Error programando el recordatorio de la cita.");
        }
        public async Task<OperationResult> SendAppointmentNotification(int userId, string message)
        {
            return await ExecuteOperationWithLogging(async () =>
            {
                var notification = new Notification
                {
                    UserID = userId,
                    Message = message,
                    SentAt = DateTime.Now
                };

                await base.Save(notification);
                SimulateEmailSending(userId, message);

                return new OperationResult
                {
                    Success = true,
                    Message = "Notificación enviada con éxito."
                };
            }, "Error enviando la notificación.");
        }
        public async Task<OperationResult> SendCustomReminder(int userId, string customMessage)
        {
            return await ExecuteOperationWithLogging(async () =>
            {
                var notification = new Notification
                {
                    UserID = userId,
                    Message = customMessage,
                    SentAt = DateTime.Now
                };

                await base.Save(notification);
                SimulateEmailSending(userId, customMessage);

                return new OperationResult
                {
                    Success = true,
                    Message = "Recordatorio enviado."
                };
            }, "Error enviando el recordatorio.");
        }

        private void SimulateEmailSending(int userId, string message)
        {
            logger.LogInformation($"...Enviando email a User: {userId}, Mensaje: {message}");
        }

    }
}