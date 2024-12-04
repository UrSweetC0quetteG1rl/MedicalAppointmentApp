using MedicalAppointment.Aplication.Contracts.Insurance;
using MedicalAppointment.Aplication.Dtos.Configuration.Insurnace.InsuranceProviders;
using MedicalAppointment.Aplication.Responses.Configuration.Appointment;
using MedicalAppointment.Persistance.Interfaces.Configuration.Insurance;
using MedicalAppointmentApp.Domain.Entities.Insurance;
using Microsoft.Extensions.Logging;



namespace MedicalAppointment.Aplication.Services.Configuration
{
    public class InsuranceProviderService : IInsuranceProviders


    {
        private readonly ILogger _logger;
        private readonly IInsuranceProvidersRepository _insuranceProviders;

        public InsuranceProviderService(IInsuranceProvidersRepository insuranceProviders, 
            ILogger<InsuranceProviderService> logger) 
        {
        
           if (insuranceProviders is null)
            {
                throw new ArgumentNullException(nameof(insuranceProviders));
            }
        
           _insuranceProviders = insuranceProviders;
           _logger = logger;
        
        }
        
         
        public async Task<InsuranceProvidersResponse> GetAll()
        {
            InsuranceProvidersResponse insuranceProviders = new InsuranceProvidersResponse();

            try
            {
                var result = await _insuranceProviders.GetAll();



                if (!result.Success) 
                {


                    insuranceProviders.IsSuccess = result.Success;
                    insuranceProviders.Message = result.Message;
                    return insuranceProviders;

                }                 
                insuranceProviders.Data = result.Data;
            }
            catch (Exception ex)
            {
                insuranceProviders.IsSuccess = false;
                insuranceProviders.Message = "Error Obteniendo seguros";
                _logger.LogError(insuranceProviders.Message, ex.ToString());


            }

            return insuranceProviders;
        }

        public async Task<InsuranceProvidersResponse> GetById(int Id)
        {
            InsuranceProvidersResponse insuranceProviders = new InsuranceProvidersResponse();

            try
            {
                var result = await _insuranceProviders.GetEntityBy(Id);

                if (!result.Success)
                {


                    insuranceProviders.IsSuccess = result.Success;
                    insuranceProviders.Message = result.Message;
                    return insuranceProviders;
                }
                insuranceProviders.Data = result.Data;


            }
            catch (Exception ex)
            {
                insuranceProviders.IsSuccess = false;
                insuranceProviders.Message = "Error Obteniendo seguros";
                _logger.LogError(insuranceProviders.Message, ex.ToString());


            }

            return insuranceProviders;

        }

        public async Task<InsuranceProvidersResponse> SaveAsync(InsuranceProvidersSaveDto dto)
        {
            InsuranceProvidersResponse insuranceProviders = new InsuranceProvidersResponse();

            try
            {
                InsuranceProviders insurance = new InsuranceProviders();


                
                insurance.Name = dto.Name;
                insurance.ContactNumber = dto.ContactNumber;
                insurance.Email = dto.Email;
                insurance.Website = dto.Website;
                insurance.Address = dto.Address;
                insurance.City = dto.City;
                insurance.State = dto.State;
                insurance.Country = dto.Country;
                insurance.ZipCode = dto.ZipCode;
                insurance.CoverageDetails = dto.CoverageDetails;
                insurance.LogoUrl = dto.LogoUrl;
                insurance.IsPreferred = dto.IsPreferred;
                insurance.NetworkTypeId = dto.NetworkTypeId;
                insurance.CustomerSupportContact = dto.CustomerSupportContact;
                insurance.AcceptedRegions = dto.AcceptedRegions;
                insurance.MaxCoverageAmount = dto.MaxCoverageAmount;
                insurance.CreatedAt = dto.CreatedAt;
                insurance.UpdatedAt = dto.UpdatedAt;
                insurance.IsActive = dto.IsActive;

                var result = await _insuranceProviders.Save(insurance);
                result.Message = "Dato guardado";





            }
            catch (Exception ex)
            {
                insuranceProviders.IsSuccess = false;
                insuranceProviders.Message = "Error guardando seguros";
                _logger.LogError(insuranceProviders.Message, ex.ToString());


            }

            return insuranceProviders;



        }

        public async Task<InsuranceProvidersResponse> UpdateAsync(InsuranceProvidersUpdateDto dto)
        {

            InsuranceProvidersResponse insuranceProviders = new InsuranceProvidersResponse();

            try
            {

                var resultGetBy = await _insuranceProviders.GetEntityBy(dto.InsuranceProviderID);

                if (!resultGetBy.Success) 
                {

                    insuranceProviders.IsSuccess = false;
                    insuranceProviders.Message = "no se encontro ";
                    return insuranceProviders;
                
                }

                InsuranceProviders insuranceToupdate = new InsuranceProviders();

                insuranceToupdate.InsuranceProviderID = dto.InsuranceProviderID;
                insuranceToupdate.Name = dto.Name;
                insuranceToupdate.ContactNumber = dto.ContactNumber;
                insuranceToupdate.Email = dto.Email;
                insuranceToupdate.Website = dto.Website;
                insuranceToupdate.Address = dto.Address;
                insuranceToupdate.City = dto.City;
                insuranceToupdate.State = dto.State;
                insuranceToupdate.Country = dto.Country;
                insuranceToupdate.ZipCode = dto.ZipCode;
                insuranceToupdate.CoverageDetails = dto.CoverageDetails;
                insuranceToupdate.LogoUrl = dto.LogoUrl;
                insuranceToupdate.IsPreferred = dto.IsPreferred;               
                insuranceToupdate.CustomerSupportContact = dto.CustomerSupportContact;
                insuranceToupdate.AcceptedRegions = dto.AcceptedRegions;
                insuranceToupdate.NetworkTypeId = dto.NetworkTypeId;
                insuranceToupdate.MaxCoverageAmount = dto.MaxCoverageAmount;               
                insuranceToupdate.UpdatedAt = dto.UpdatedAt;
                insuranceToupdate.IsActive = dto.IsActive;



                var result = await _insuranceProviders.Update(insuranceToupdate);
                result.Message = "Actualizado";





            }
            catch (Exception ex)
            {
                insuranceProviders.IsSuccess = false;
                insuranceProviders.Message = "Error actualizando seguros";
                _logger.LogError(insuranceProviders.Message, ex.ToString());


            }

            return insuranceProviders;




        }
    }
}
