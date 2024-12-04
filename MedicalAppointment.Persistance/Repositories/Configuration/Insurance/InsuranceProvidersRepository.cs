using MedicalAppointment.Persistance.Base;
using MedicalAppointment.Persistance.Context;
using MedicalAppointment.Persistance.Interfaces.Configuration.Insurance;
using MedicalAppointment.Persistance.Models;
using MedicalAppointment.Persistance.Models.Insurnaces;
using MedicalAppointmentApp.Domain.Entities.Appoinments;
using MedicalAppointmentApp.Domain.Entities.Insurance;
using MedicalAppointmentApp.Domain.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;




namespace MedicalAppointment.Persistance.Repositories.Configuration.Insurance
{
    public sealed class InsuranceProvidersRepository(MedicalAppointmentContext InsuranceContext, ILogger<InsuranceProvidersRepository>logger)
        : BaseRepository<InsuranceProviders>(InsuranceContext), IInsuranceProvidersRepository
    {

        private readonly MedicalAppointmentContext InsuranceContext = InsuranceContext;
        private readonly ILogger<InsuranceProvidersRepository> logger = logger;


        public async override Task<OperationResult> Save(InsuranceProviders entity)
        {

            OperationResult result = new OperationResult();



            result = ValidarEntity(entity);
            if(!result.Success) return result;



          /*  if (await base.Exists(insurance => insurance.Name == entity.Name)) 
            {

                result.Success = false;
                result.Message = "Ya existe ese seguro";
                return result;


            }*/

            try 
            {

                
                result = await base.Save(entity);
                result.Success = true;
            }

            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrió un error al guardar el seguro.";
                this.logger.LogError(result.Message, ex.ToString());
            }         
            return result;    
        }

        public async override Task<OperationResult> Update(InsuranceProviders entity)
        {
            OperationResult result = new OperationResult();


            result = ValidarEntity(entity);
            if (!result.Success) return result;


            try 
            {

                InsuranceProviders? insuranceProviderToUpdate = await InsuranceContext.InsuranceProviders.FindAsync(entity.InsuranceProviderID);

                if (entity == null) 
                {

                    result.Success = false;
                    result.Message = "La entidad no puede ser null";
                    return result;

                }

                if (entity.InsuranceProviderID <= 0)
                {

                    result.Success = false;
                    result.Message = "Se requiere el Id del seguro";
                    return result;

                }


                insuranceProviderToUpdate.InsuranceProviderID = entity.InsuranceProviderID;
                insuranceProviderToUpdate.Name = entity.Name;
                insuranceProviderToUpdate.ContactNumber = entity.ContactNumber;
                insuranceProviderToUpdate.Email = entity.Email;
                insuranceProviderToUpdate.Website = entity.Website;
                insuranceProviderToUpdate.Address = entity.Address;
                insuranceProviderToUpdate.City = entity.City;
                insuranceProviderToUpdate.State = entity.State;
                insuranceProviderToUpdate.Country = entity.Country;
                insuranceProviderToUpdate.ZipCode = entity.ZipCode;
                insuranceProviderToUpdate.CoverageDetails = entity.CoverageDetails;
                insuranceProviderToUpdate.LogoUrl = entity.LogoUrl;
                insuranceProviderToUpdate.IsPreferred = entity.IsPreferred;
                insuranceProviderToUpdate.CustomerSupportContact = entity.CustomerSupportContact;
                insuranceProviderToUpdate.AcceptedRegions = entity.AcceptedRegions;
                insuranceProviderToUpdate.NetworkTypeId = entity.NetworkTypeId;
                insuranceProviderToUpdate.MaxCoverageAmount = entity.MaxCoverageAmount;
                insuranceProviderToUpdate.UpdatedAt = DateTime .Now;
                insuranceProviderToUpdate.IsActive = entity.IsActive;

                result = await base.Update(insuranceProviderToUpdate);
            
            }
            
            
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrió un error al actualizar el seguro.";
                logger.LogError(result.Message, ex.ToString);
            }


            return result;

        }


        public async override Task<OperationResult> Remove(InsuranceProviders entity)
        {

            OperationResult result = new OperationResult();

            result = ValidarEntity(entity);
            if (!result.Success) return result;


           

            if (entity.InsuranceProviderID <= 0)
            {

                result.Success = false;
                result.Message = "Debe ingresar el ID del seguro que desea eliminar";
                return result;

            }


            try
            {

                InsuranceProviders? insuranceProviderToRemove = await InsuranceContext.InsuranceProviders.FindAsync(entity.InsuranceProviderID);


                
                insuranceProviderToRemove.UpdatedAt = DateTime .Now;

                await base.Update(insuranceProviderToRemove);
           
            }

            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrió un error al Eliminar el seguro.";
                logger.LogError(result.Message, ex.ToString);
            }

            return result;


        }


        public async override Task<OperationResult> GetAll()
        {
          
            OperationResult result = new OperationResult();


            try
            {

                result.Data = await (from InsuranceProvider in InsuranceContext.InsuranceProviders
                                     join NetworkType in InsuranceContext.NetworkType on InsuranceProvider.NetworkTypeId equals NetworkType.NetworkTypeId
                                     where InsuranceProvider.InsuranceProviderID >= 0
                                     orderby InsuranceProvider.CreatedAt descending
                                     select new InsuranceProvidersNetworkModel()
                                     {


                                         InsuranceProviderID = InsuranceProvider.InsuranceProviderID,
                                         NetworkTypeId = NetworkType.NetworkTypeId,
                                         Name = InsuranceProvider.Name,
                                         ContactNumber = InsuranceProvider.ContactNumber,
                                         Email = InsuranceProvider.Email,
                                         Website = InsuranceProvider.Website,
                                         Address = InsuranceProvider.Address,
                                         City = InsuranceProvider.City,
                                         State = InsuranceProvider.State,
                                         Country = InsuranceProvider.Country,
                                         ZipCode = InsuranceProvider.ZipCode,
                                         CoverageDetails = InsuranceProvider.CoverageDetails,
                                         LogoUrl = InsuranceProvider.LogoUrl,
                                         IsPreferred = InsuranceProvider.IsPreferred,
                                         CustomerSupportContact = InsuranceProvider.CustomerSupportContact,
                                         AcceptedRegions = InsuranceProvider.AcceptedRegions,                                         
                                         MaxCoverageAmount = InsuranceProvider.MaxCoverageAmount,
                                         CreatedAt = InsuranceProvider.CreatedAt,
                                         UpdatedAt = InsuranceProvider.UpdatedAt,
                                         IsActive = InsuranceProvider.IsActive,
                                         

                                     }).AsNoTracking()
                                     .ToListAsync();


            }
            catch (Exception ex) 
            {

                result.Success = false;
                result.Message = "Error obteniendo los seguros";
                logger.LogError(result.Message, ex.ToString());

            }


            return result;

        }


        public async override Task<OperationResult> GetEntityBy(int ID)
        {
            OperationResult result = new OperationResult();


            try
            {

                result.Data = await (from InsuranceProvider in InsuranceContext.InsuranceProviders join NetworkType in InsuranceContext.NetworkType on 
                                     InsuranceProvider.NetworkTypeId equals NetworkType.NetworkTypeId
                                     where InsuranceProvider.InsuranceProviderID == ID
                                     select new InsuranceProvidersNetworkModel()
                                     {


                                         InsuranceProviderID = InsuranceProvider.InsuranceProviderID,

                                         Name = InsuranceProvider.Name,
                                         ContactNumber = InsuranceProvider.ContactNumber,
                                         Email = InsuranceProvider.Email,
                                         Website = InsuranceProvider.Website,
                                         Address = InsuranceProvider.Address,
                                         City = InsuranceProvider.City,
                                         State = InsuranceProvider.State,
                                         Country = InsuranceProvider.Country,
                                         ZipCode = InsuranceProvider.ZipCode,
                                         CoverageDetails = InsuranceProvider.CoverageDetails,
                                         LogoUrl = InsuranceProvider.LogoUrl,
                                         IsPreferred = InsuranceProvider.IsPreferred,
                                         CustomerSupportContact = InsuranceProvider.CustomerSupportContact,
                                         AcceptedRegions = InsuranceProvider.AcceptedRegions,
                                         NetworkTypeId = InsuranceProvider.NetworkTypeId,
                                         MaxCoverageAmount = InsuranceProvider.MaxCoverageAmount,
                                         CreatedAt = InsuranceProvider.CreatedAt,
                                         UpdatedAt = InsuranceProvider.UpdatedAt,
                                         IsActive = InsuranceProvider.IsActive,
                                         

                                     }).AsNoTracking()
                                     .FirstOrDefaultAsync();


            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Message = "Error obteniendo los seguros";
                logger.LogError(result.Message, ex.ToString());

            }


            return result;
        }


        private OperationResult ValidarEntity(InsuranceProviders entity)
        {
            OperationResult result = new OperationResult();


            if (entity == null ) 
            {
                result.Success = false;
                result.Message = "La entidad no puede ser nula";
                return result;

            }

            if (string.IsNullOrEmpty(entity.Name))
            {
                result.Success = false;
                result.Message = "Necesita ingresar un nombre";
                return result;

            }

            if (string.IsNullOrEmpty(entity.ContactNumber))
            {

                result.Success = false;
                result.Message = "Necesita ingresar un nombre";
                return result;

            }

            if (string.IsNullOrEmpty(entity.Email))
            {

                result.Success = false;
                result.Message = "Necesita ingresar un Correo";
                return result;


            }

            if (string.IsNullOrEmpty(entity.Website))
            {
                result.Success = false;
                result.Message = "Necesita ingresar el Website";
                return result;
            }

            if (string.IsNullOrEmpty(entity.Address))
            {
                result.Success = false;
                result.Message = "Necesita ingresar una direccion";
                return result;
            }

            if (string.IsNullOrEmpty(entity.City))
            {
                result.Success = false;
                result.Message = "Necesita ingresar una ciudad";
                return result;
            }

            if (string.IsNullOrEmpty(entity.State))
            {
                result.Success = false;
                result.Message = "Necesita ingresar su Estado";
                return result;
            }
            if (string.IsNullOrEmpty(entity.ZipCode))
            {
                result.Success = false;
                result.Message = "Necesita ingresar un codigo postal";
                return result;
            }
            if (string.IsNullOrEmpty(entity.Country))
            {
                result.Success = false;
                result.Message = "Necesita ingresar un Pais";
                return result;
            }
            if (string.IsNullOrEmpty(entity.CoverageDetails))
            {
                result.Success = false;
                result.Message = "Ingrese los detalles";
                return result;
            }
            if (string.IsNullOrEmpty(entity.LogoUrl))
            {
                result.Success = false;
                result.Message = "Necesita ingresar una DireccionURL";
                return result;
            }

            if (string.IsNullOrEmpty(entity.CustomerSupportContact))
            {
                result.Success = false;
                result.Message = "Ingrese contacto de Soporte";
                return result;
            }

            if (string.IsNullOrEmpty(entity.AcceptedRegions))
            {
                result.Success = false;
                result.Message = "Ingrese Regiones aceptadas";
                return result;

            }

            if (entity.MaxCoverageAmount == null)
            {

                result.Success = false;
                result.Message = "Necesita ingresar un monto";
                return result;

            }

            if (entity.MaxCoverageAmount <= 0)
            {

                result.Success = false;
                result.Message = "El monto no puede ser negativo";
                return result;

            }

            result.Success = true;
            return result;
        }




    }
}
