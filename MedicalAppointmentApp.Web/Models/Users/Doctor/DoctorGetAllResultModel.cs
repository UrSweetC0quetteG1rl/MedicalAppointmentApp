using MedicalAppointment.Persistance.Models.User;

namespace MedicalAppointmentApp.Web.Models.Users.Doctor
{
    public class DoctorGetAllResultModel: BaseApiResponseModel
    {
        public List<DoctorSpecialtyAvailabilityModel> data { get; set; }
    }
}
