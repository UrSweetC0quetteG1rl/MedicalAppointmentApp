

using MedicalAppointmentApp.Domain.Entities.User;
using MedicalAppointmentApp.Domain.Repositories;
using MedicalAppointmentApp.Domain.Result;

namespace MedicalAppointment.Persistance.Interfaces.Users
{
    public interface IPatientRepository: IBaseRepository<Patient>
    {
        Task<OperationResult> GetPatientById(int patientId);
    }
}
