using MedicalAppointment.Persistance.Models.User;

namespace MedicalAppointmentApp.Web.Models.Users.Patient
{
    public class PatientGetAllResultModel: BaseApiResponseModel
    {
        public List<PatientInsurenceProviderModel> data { get; set; }
    }
}
