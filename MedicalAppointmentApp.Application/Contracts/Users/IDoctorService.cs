using MedicalAppointmentApp.Application.Base;
using MedicalAppointmentApp.Application.Dtos.Users.Doctor;
using MedicalAppointmentApp.Application.Responses.Users.Doctor;


namespace MedicalAppointmentApp.Application.Contracts.Users
{
    public interface IDoctorService : IBaseService<DoctorResponse, DoctorDtoSave, DoctorDtoUpdate>
    {
    }
}
