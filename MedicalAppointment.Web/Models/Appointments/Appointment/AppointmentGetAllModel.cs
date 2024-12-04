using MedicalAppointment.Persistance.Models.Appointments;
using MedicalAppointment.Web.Models.Core;

namespace MedicalAppointment.Web.Models.Appointments.Appointment
{
    public class AppointmentGetAllModel : BaseApiModel
    {
        public List<AppointmentDoctorModel> Data { get; set; } 
    }
}
