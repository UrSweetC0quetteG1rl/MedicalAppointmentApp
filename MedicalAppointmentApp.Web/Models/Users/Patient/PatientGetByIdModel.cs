using MedicalAppointment.Persistance.Models.User;

namespace MedicalAppointmentApp.Web.Models.Users.Patient
{
    public class PatientGetByIdModel: BaseApiResponseModel
    {
        public PatientInsurenceProviderModel data { get; set; }
    }
}
