using MedicalAppointment.Aplication.Base;
using MedicalAppointment.Aplication.Dtos.Configuration.Appointment.DoctorAvailability;
using MedicalAppointment.Aplication.Responses.Configuration.DoctorAvailability;


namespace MedicalAppointment.Aplication.Contracts.Appointment
{
    public interface IDoctorAvailability : IBaseService<DoctorAvailabilityResponse, DoctorAvailabilitySaveDto, DoctorAvailabilityUpdateDto>
    {
    }
}
