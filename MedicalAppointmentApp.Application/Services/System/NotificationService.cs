

using MedicalAppointment.Persistance.Interfaces.System;
using MedicalAppointmentApp.Application.Contracts.System;
using MedicalAppointmentApp.Application.Dtos.System.Notification;
using MedicalAppointmentApp.Application.Responses.System.Notification;
using MedicalAppointmentApp.Domain.Entities.System;
using Microsoft.Extensions.Logging;

namespace MedicalAppointmentApp.Application.Services.System
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(INotificationRepository notificationRepository, ILogger<NotificationService> logger)
        {
            if (notificationRepository is null) { throw new ArgumentNullException(nameof(notificationRepository)); }
            _notificationRepository = notificationRepository;
            _logger = logger;
        }

        public async Task<NotificationResponse> GetAll()
        {
            NotificationResponse notificationResponse = new NotificationResponse();

            try
            {
                var result = await _notificationRepository.GetAll();

                if (!result.Success)
                {
                    notificationResponse.Message = result.Message;
                    notificationResponse.IsSuccess = result.Success;
                    return notificationResponse;
                }
                notificationResponse.Data = result.Data;
            }
            catch (Exception ex)
            {
                notificationResponse.IsSuccess = false;
                notificationResponse.Message = "Error obteninedo los Notifications";
                _logger.LogError(notificationResponse.Message, ex.ToString());
            }
            return notificationResponse;
        }

        public async Task<NotificationResponse> GetById(int Id)
        {
            NotificationResponse notificationResponse = new NotificationResponse();

            try
            {
                var result = await _notificationRepository.GetEntityBy(Id);

                if (!result.Success)
                {
                    notificationResponse.Message = result.Message;
                    notificationResponse.IsSuccess = result.Success;
                    return notificationResponse;
                }
                notificationResponse.Data = result.Data;
            }
            catch (Exception ex)
            {
                notificationResponse.IsSuccess = false;
                notificationResponse.Message = "Error obteninedo la notificacion";
                _logger.LogError(notificationResponse.Message, ex.ToString());
            }
            return notificationResponse;
        }

        public async Task<NotificationResponse> SaveAsync(NotificationDtoSave dto)
        {

            NotificationResponse notificationResponse = new NotificationResponse();

            try
            {
                Notification notification = new Notification();

                notification.NotificationID = dto.NotificationID;
                notification.UserID = dto.UserID;
                notification.SentAt = dto.SentAt;
                notification.Message = dto.Message;

            }
            catch (Exception ex)
            {
                notificationResponse.IsSuccess = false;
                notificationResponse.Message = "Error guardando la notificacion.";
                _logger.LogError(notificationResponse.Message, ex.ToString());
            }
            return notificationResponse;
        }

        public async Task<NotificationResponse> UpdateAsync(NotificationDtoUpdate dto)
        {
            NotificationResponse notificationResponse = new NotificationResponse();

            try
            {
                var resultGet = await _notificationRepository.GetEntityBy(dto.NotificationID);

                if (!resultGet.Success)
                {
                    notificationResponse.IsSuccess = resultGet.Success;
                    notificationResponse.Message = resultGet.Message;
                    return notificationResponse;
                }

                Notification notification = (Notification)resultGet.Data;

                notification.NotificationID = dto.NotificationID;
                notification.Message = dto.Message;
                notification.SentAt = dto.SentAt;
                
            }
            catch (Exception ex)
            {
                notificationResponse.IsSuccess = false;
                notificationResponse.Message = "Error actualizando la notificacion.";
                _logger.LogError(notificationResponse.Message, ex.ToString());
            }
            return notificationResponse;
        }
    
    }
}
