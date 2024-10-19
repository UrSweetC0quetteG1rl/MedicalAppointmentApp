


using MedicalAppointment.Persistance.Base;
using MedicalAppointment.Persistance.Context;
using MedicalAppointment.Persistance.Interfaces.Configuration.Insurance;
using MedicalAppointment.Persistance.Models;
using MedicalAppointment.Persistance.Repositories.Configuration.Appointments;
using MedicalAppointmentApp.Domain.Entities.Appoinments;
using MedicalAppointmentApp.Domain.Entities.Insurance;
using MedicalAppointmentApp.Domain.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedicalAppointment.Persistance.Repositories.Configuration.Insurance
{
    public sealed class NetworkTypeRepository : BaseRepository<NetworkType>, INetworkTypeRepository
    {
        private readonly MedicalAppointmentContext _networkContext;
        private readonly ILogger<NetworkTypeRepository> _logger;



        public NetworkTypeRepository(MedicalAppointmentContext networkContext, ILogger<NetworkTypeRepository> logger)
            : base(networkContext)
        {
            _networkContext = networkContext;
            _logger = logger;
        }









        public async override Task<OperationResult> Save(NetworkType entity)
        {

            OperationResult operationResult = new OperationResult();


            if (entity == null)
            {

                operationResult.Success = false;
                operationResult.Message = "Esta entidad no Puede ser Nula.";
                return operationResult;

            }


            if (entity.Name == null)
            {

                operationResult.Success = false;
                operationResult.Message = "Debe Ingresar un nombre.";
                return operationResult;
            }

            if (entity.Description == null)
            {
                operationResult.Success = false;
                operationResult.Message = "Debe agregar una descripcion.";
                return operationResult;
            }

            try
            {


                operationResult = await base.Save(entity);
                
            }
            catch (Exception ex)
            {

                operationResult.Success = false;
                operationResult.Message = "Ocurrió un error al guardar.";
                _logger.LogError(operationResult.Message, ex.ToString());

                return operationResult;
            }
            return operationResult;
        }
        



        public async override Task<OperationResult> Update(NetworkType entity)
        {
            OperationResult operationResult = new OperationResult();


            operationResult = ValidarNetWorkType(entity);
            if (!operationResult.Success) return operationResult;

            try
            {


                NetworkType? networkTypeToUpdate = await _networkContext.NetworkType.FindAsync(entity.NetworkTypeId);

                if (networkTypeToUpdate == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "Debe Ingresar un ID.";
                    return operationResult;
                }


                networkTypeToUpdate.Description = entity.Description;
                networkTypeToUpdate.Name = entity.Name;
                networkTypeToUpdate.UpdatedAt = DateTime.Now;
                networkTypeToUpdate.IsActive = entity.IsActive;

                operationResult = await base.Update(networkTypeToUpdate);


            }

            catch (Exception ex)
            {

                operationResult.Success = false;
                operationResult.Message = "Ocurrió un error al Reprogramar la cita.";
                _logger.LogError(operationResult.Message, ex.ToString());

            }

            return operationResult;
        }




        public async override Task<OperationResult> Remove(NetworkType entity)
        {
            OperationResult operationResult = new OperationResult();

            try
            {


                NetworkType? networkTypeToRemove = await _networkContext.NetworkType.FindAsync(entity.NetworkTypeId);

                if (networkTypeToRemove == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "Debe Ingresar un ID.";
                    return operationResult;
                }


                if (entity.NetworkTypeId <= 0)
                {
                    operationResult.Success = false;
                    operationResult.Message = "No se encontró  con el ID proporcionado.";
                    return operationResult;
                }

                networkTypeToRemove.IsActive = false;
                networkTypeToRemove.UpdatedAt = entity.UpdatedAt;

                operationResult = await base.Update(networkTypeToRemove);


            }

            catch (Exception ex)
            {

                operationResult.Success = false;
                operationResult.Message = "Ocurrió un error al eliminar .";
                _logger.LogError(operationResult.Message, ex.ToString());

            }

            return operationResult;

        }



       public async override Task<OperationResult> GetAll()
        {
            OperationResult operationResult = new OperationResult();

            try
            {

                operationResult.Data = await (from NetworkType in _networkContext.NetworkType
                                              where NetworkType.IsActive == true
                                              orderby NetworkType.CreatedAt descending
                                              select new NetworkTypeModel
                                              {

                                                  NetworkTypeId = NetworkType.NetworkTypeId,
                                                  Name = NetworkType.Name,
                                                  Description = NetworkType.Description,                                               
                                                  CreatedAt = NetworkType.CreatedAt,
                                                  UpdateAt = NetworkType.UpdatedAt,
                                                  IsActive = NetworkType.IsActive



                                              }).ToListAsync(); ;

            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error obteniendo Network";
                _logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }


        private OperationResult ValidarNetWorkType(NetworkType entity)
        {
            OperationResult operationResult = new OperationResult();



            if (entity == null)
            {

                operationResult.Success = false;
                operationResult.Message = "Esta entidad no Puede ser Nula.";
                return operationResult;


            }


            if (entity.Name == null)
            {

                operationResult.Success = false;
                operationResult.Message = "Debe Ingresar un nombre.";
                return operationResult;


            }

            if (entity.Description == null)
            {
                operationResult.Success = false;
                operationResult.Message = "Debe agregar una descripcion.";
                return operationResult;
            }

            operationResult.Success = true;
            return operationResult;

        }
    }
}
