
using MedicalAppointmentApp.Domain.Entities.User;
using MedicalAppointmentApp.Domain.Repositories;
using MedicalAppointmentApp.Domain.Result;

namespace MedicalAppointment.Persistance.Interfaces.Users
{
    public interface IDoctorRepository: IBaseRepository<Doctor>
    {
        Task<OperationResult> GetDoctorBySpecialty(int specialtyId);
        Task<OperationResult> GetDoctorByHour(DateTime date, TimeSpan hour);
    }
}
