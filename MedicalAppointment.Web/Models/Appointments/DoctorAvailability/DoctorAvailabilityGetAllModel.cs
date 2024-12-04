using MedicalAppointment.Persistance.Models.Appointments;
using MedicalAppointment.Web.Models.Core;

namespace MedicalAppointment.Web.Models.Appointments.DoctorAvailability
{
    public class DoctorAvailabilityGetAllModel : BaseApiModel
    {
        public List<DoctorAvailabilityModel>? Data { get; set; }
    }
}
