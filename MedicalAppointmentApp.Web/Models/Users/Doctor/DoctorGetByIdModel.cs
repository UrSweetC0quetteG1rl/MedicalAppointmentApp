using MedicalAppointment.Persistance.Models.User;

namespace MedicalAppointmentApp.Web.Models.Users.Doctor
{
    public class DoctorGetByIdModel: BaseApiResponseModel
    {
        public DoctorSpecialtyAvailabilityModel data { get; set; }
    }
}
