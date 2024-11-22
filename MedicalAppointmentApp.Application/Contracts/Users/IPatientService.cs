


using MedicalAppointmentApp.Application.Base;
using MedicalAppointmentApp.Application.Dtos.Users.Patient;
using MedicalAppointmentApp.Application.Responses.Users.Patient;

namespace MedicalAppointmentApp.Application.Contracts.Users
{
    public interface IPatientService : IBaseService<PatientResponse, PatientDtoSave, PatientDtoUpdate>
    {
    }
}
