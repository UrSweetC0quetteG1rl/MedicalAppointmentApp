using MedicalAppointment.Aplication.Base;
using MedicalAppointment.Aplication.Dtos.Configuration.Appointment;
using MedicalAppointment.Aplication.Responses.Configuration.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointment.Aplication.Contracts
{
    public interface IInsuranceProviders : IBaseService<InsuranceProvidersResponse, InsuranceProvidersSaveDto, InsuranceProvidersUpdateDto>
    {
    }
}
