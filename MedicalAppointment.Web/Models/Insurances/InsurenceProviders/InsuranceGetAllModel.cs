using MedicalAppointment.Persistance.Models.Insurnaces;
using MedicalAppointment.Web.Models.Core;

namespace MedicalAppointment.Web.Models.Insurances.InsurenceProviders
{
    public class InsuranceGetAllModel : BaseApiModel
    {
        public List<InsuranceProvidersNetworkModel> Data { get; set; }




    }
}
