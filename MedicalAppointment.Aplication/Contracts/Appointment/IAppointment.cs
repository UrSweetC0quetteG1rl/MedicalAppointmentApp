using MedicalAppointment.Aplication.Base;
using MedicalAppointment.Aplication.Dtos.Configuration.Appointment.Appointments;
using MedicalAppointment.Aplication.Responses.Configuration.Appointment;


namespace MedicalAppointment.Aplication.Contracts.Appointment
{
    public interface IAppointment : IBaseService<AppointmentResponse, AppointmentSaveDto, AppointmentUpdateDto>
    {
    }
}
