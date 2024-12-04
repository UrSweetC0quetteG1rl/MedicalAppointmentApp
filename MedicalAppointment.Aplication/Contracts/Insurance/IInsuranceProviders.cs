using MedicalAppointment.Aplication.Base;
using MedicalAppointment.Aplication.Dtos.Configuration.Insurnace.InsuranceProviders;
using MedicalAppointment.Aplication.Responses.Configuration.Appointment;

namespace MedicalAppointment.Aplication.Contracts.Insurance
{
    public interface IInsuranceProviders : IBaseService<InsuranceProvidersResponse, InsuranceProvidersSaveDto, InsuranceProvidersUpdateDto>
    {
    }
}
