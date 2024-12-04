using MedicalAppointment.Persistance.Models.Insurnaces;
using MedicalAppointment.Web.Models.Core;

namespace MedicalAppointment.Web.Models.Insurances.InsurenceProviders
{
    public class InsuranceProvidersGetByIDModel : BaseApiModel
    {
        public InsuranceProvidersNetworkModel? data { get; set; }
    }
}
