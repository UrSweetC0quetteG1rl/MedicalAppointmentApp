using MedicalAppointment.Persistance.Models.Appointments;
using MedicalAppointment.Web.Models.Core;

namespace MedicalAppointment.Web.Models.Appointments.DoctorAvailability
{
    public class DoctorAvailabilityGetByID : BaseApiModel
    {
        public DoctorAvailabilityModel? data { get; set; }

    }
}
