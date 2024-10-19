

using MedicalAppointment.Persistance.Base;
using MedicalAppointment.Persistance.Context;
using MedicalAppointment.Persistance.Interfaces.Configuration.Medical;
using MedicalAppointment.Persistance.Models;
using MedicalAppointmentApp.Domain.Entities.Medical;
using MedicalAppointmentApp.Domain.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedicalAppointment.Persistance.Repositories.Configuration.Medical
{
    public class AvailabilityModesRepository(MedicalAppointmentContext avialabilityModeContext, ILogger<AvailabilityModesRepository> logger)
        : BaseRepository<AvailabilityModes>(avialabilityModeContext), IAvailabilityModesRepository

    {
        private readonly MedicalAppointmentContext avialabilityModeContext;
        private readonly ILogger<AvailabilityModesRepository> logger;



        public async override Task<OperationResult> Save(AvailabilityModes entity)
        {
            OperationResult operationResult = new OperationResult();


            operationResult = ValidarAvialaMode(entity);
            if (!operationResult.Success) return operationResult;



            if (await base.Exists(avialabilityModeContext => avialabilityModeContext.AvailabilityMode == entity.AvailabilityMode))
            {

                operationResult.Success = false;
                operationResult.Message = "Ya existe.";
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
                this.logger.LogError(operationResult.Message, ex.ToString());


                return operationResult;
            }

            return operationResult;

        }


        public async override Task<OperationResult> Update(AvailabilityModes entity)
        {

            OperationResult operationResult = new OperationResult();

            operationResult = ValidarAvialaMode(entity);
            if (!operationResult.Success) return operationResult;

            try
            {


                AvailabilityModes? availaModeToUpdate = await avialabilityModeContext.AvailabilityModes.FindAsync(entity.SAvailabilityModeID);

                if (availaModeToUpdate == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "Debe Ingresar un ID.";
                    return operationResult;
                }


                if (entity.SAvailabilityModeID <= 0)
                {
                    operationResult.Success = false;
                    operationResult.Message = "No se encontró  con el ID proporcionado.";
                    return operationResult;
                }

                availaModeToUpdate.AvailabilityMode = entity.AvailabilityMode;
                availaModeToUpdate.UpdatedAt = entity.UpdatedAt;
                availaModeToUpdate.IsActive = entity.IsActive;

                operationResult = await base.Update(availaModeToUpdate);


            }

            catch (Exception ex)
            {

                operationResult.Success = false;
                operationResult.Message = "Ocurrió un error al Reprogramar la cita.";
                logger.LogError(operationResult.Message, ex.ToString);

            }

            return operationResult;
        }





        public async override Task<OperationResult> Remove(AvailabilityModes entity)
        {
            OperationResult operationResult = new OperationResult();

            operationResult = ValidarAvialaMode(entity);
            if (!operationResult.Success) return operationResult;


            try
            {


                AvailabilityModes? availabilityModeToRemove = await avialabilityModeContext.AvailabilityModes.FindAsync(entity.SAvailabilityModeID);



                if (availabilityModeToRemove == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "Debe Ingresar un ID.";
                    return operationResult;
                }


                if (entity.SAvailabilityModeID <= 0)
                {
                    operationResult.Success = false;
                    operationResult.Message = "No se encontró  con el ID proporcionado.";
                    return operationResult;
                }

                availabilityModeToRemove.IsActive = false;
                availabilityModeToRemove.UpdatedAt = entity.UpdatedAt;

                operationResult = await base.Update(availabilityModeToRemove);


            }

            catch (Exception ex)
            {

                operationResult.Success = false;
                operationResult.Message = "Ocurrió un error al eliminar .";
                logger.LogError(operationResult.Message, ex.ToString);

            }

            return operationResult;
        }


        public async override Task<OperationResult> GetAll()
        {
            OperationResult operationResult = new OperationResult();



            try
            {

                operationResult.Data = await (from AvailabilityModes in avialabilityModeContext.AvailabilityModes
                                              where AvailabilityModes.IsActive == true
                                              orderby AvailabilityModes.CreatedAt descending
                                              select new AvailabilityModesModel()
                                              {


                                                  SAvailabilityModeID = AvailabilityModes.SAvailabilityModeID,
                                                  AvailabilityMode = AvailabilityModes.AvailabilityMode,
                                                  CreatedAt = AvailabilityModes.CreatedAt,
                                                  UpdatedAt = AvailabilityModes.UpdatedAt,
                                                  IsActive = AvailabilityModes.IsActive



                                              }

                                              ).ToListAsync(); ;




            }


            catch (Exception ex)
            {

                operationResult.Success = false;
                operationResult.Message = "Error obteniendo los datos";
                logger.LogError(operationResult.Message, ex.ToString());

            }

            return operationResult;
        }

        private OperationResult ValidarAvialaMode(AvailabilityModes entity)
        {
            OperationResult operationResult = new OperationResult();



            if (entity == null)
            {

                operationResult.Success = false;
                operationResult.Message = "Esta entidad no Puede ser Nula.";
                return operationResult;


            }


            if (entity.AvailabilityMode == null)
            {

                operationResult.Success = false;
                operationResult.Message = "Debe Ingresar un Modo.";
                return operationResult;


            }



            operationResult.Success = false;
            return operationResult;


        }



    }
}