using MedicalAppointmentApp.Application.Base;
using MedicalAppointmentApp.Application.Dtos.System.Notification;
using MedicalAppointmentApp.Application.Responses.System.Notification;


namespace MedicalAppointmentApp.Application.Contracts.System
{
    public interface INotificationService: IBaseService<NotificationResponse, NotificationDtoSave, NotificationDtoUpdate>
    {
    }
}
