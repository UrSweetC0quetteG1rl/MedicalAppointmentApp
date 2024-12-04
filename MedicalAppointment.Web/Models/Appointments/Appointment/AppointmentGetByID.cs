using MedicalAppointment.Persistance.Models.Appointments;
using MedicalAppointment.Web.Models.Core;

namespace MedicalAppointment.Web.Models.Appointments.Appointment
{
    public class AppointmentGetByID : BaseApiModel
    {
        public AppointmentDoctorModel? data { get; set; }

    }
}
