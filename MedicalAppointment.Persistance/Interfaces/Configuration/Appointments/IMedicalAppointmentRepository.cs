using MedicalAppointmentApp.Domain.Entities.Appoinments;
using MedicalAppointmentApp.Domain.Repositories;
using MedicalAppointmentApp.Domain.Result;

namespace MedicalAppointment.Persistance.Interfaces.Configuration.Appointments
{
    public interface IMedicalAppointmentRepository : IBaseRepository<MedicalAppointmentApp.Domain.Entities.Appoinments.Appointments>
    {

    }
}
