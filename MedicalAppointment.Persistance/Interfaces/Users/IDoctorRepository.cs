
using MedicalAppointmentApp.Domain.Entities.User;
using MedicalAppointmentApp.Domain.Repositories;
using MedicalAppointmentApp.Domain.Result;

namespace MedicalAppointment.Persistance.Interfaces.Users
{
    public interface IDoctorRepository: IBaseRepository<Doctor>
    {
        Task<OperationResult> GetDoctorsBySpecialty(short specialtyId);
        Task<OperationResult> DeactivateDoctorById(int doctorId);
    }
}
