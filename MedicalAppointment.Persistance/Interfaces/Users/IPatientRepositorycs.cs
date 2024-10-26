

using MedicalAppointmentApp.Domain.Entities.User;
using MedicalAppointmentApp.Domain.Repositories;

namespace MedicalAppointment.Persistance.Interfaces.Users
{
    public interface IPatientRepository: IBaseRepository<Patient>
    {
    }
}
