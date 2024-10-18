using MedicalAppointmentApp.Domain.Entities.System;
using MedicalAppointmentApp.Domain.Repositories;
using MedicalAppointmentApp.Domain.Result;

namespace MedicalAppointment.Persistance.Interfaces.System
{
    public interface INotificationRepository : IBaseRepository<Notification>
    {
        Task<OperationResult> SendAppointmentNotification(int userId, string message);
        Task<OperationResult> ScheduleReminder(int userId, string message, DateTime reminderDate);
        Task<OperationResult> SendCustomReminder(int userId, string customMessage);
    }
}
