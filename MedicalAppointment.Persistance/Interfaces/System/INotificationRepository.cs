using MedicalAppointmentApp.Domain.Entities.System;
using MedicalAppointmentApp.Domain.Repositories;
using MedicalAppointmentApp.Domain.Result;

namespace MedicalAppointment.Persistance.Interfaces.System
{
    public interface INotificationRepository : IBaseRepository<Notification>
    {
    }
}
