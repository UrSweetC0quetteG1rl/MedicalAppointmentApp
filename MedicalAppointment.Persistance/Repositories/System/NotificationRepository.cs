using MedicalAppointment.Persistance.Base;
using MedicalAppointment.Persistance.Context;
using MedicalAppointment.Persistance.Interfaces.System;
using MedicalAppointmentApp.Domain.Entities.System;
using MedicalAppointmentApp.Domain.Result;
using Microsoft.Extensions.Logging;

namespace MedicalAppointment.Persistance.Repositories.System
{
    public sealed class NotificationRepository: BaseRepository<Notification>, INotificationRepository
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private readonly ILogger<NotificationRepository> _logger;

        public NotificationRepository(MedicalAppointmentContext medicalAppointmentContext, ILogger<NotificationRepository> logger)
        : base(medicalAppointmentContext)
        {
            _medicalAppointmentContext = medicalAppointmentContext;
            _logger = logger;
        }




    }
}