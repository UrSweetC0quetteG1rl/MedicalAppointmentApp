using MedicalAppointment.Persistance.Base;
using MedicalAppointment.Persistance.Context;
using MedicalAppointmentApp.Domain.Entities.System;
using MedicalAppointmentApp.Domain.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedicalAppointment.Persistance.Repositories.System
{
    public sealed class StatusRepository(MedicalAppointmentContext medicalAppointmentContext, ILogger<StatusRepository> logger) 
        : BaseRepository<Status>(medicalAppointmentContext)
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private readonly ILogger<RoleRepository> logger;

        private OperationResult ValidateStatusEntity(Status entity)
        {
            OperationResult result = new OperationResult();

            if (entity == null)
            {
                result.Success = false;
                result.Message = "La entidad es requerida.";
                return result;
            }
            if (string.IsNullOrEmpty(entity.StatusName))
            {
                result.Success = false;
                result.Message = "El nombre del estado es necesario.";
                return result;
            }

            result.Success = true;
            return result;

        }
        private async Task<OperationResult> ExecuteOperationWithLogging(Func<Task<OperationResult>> operation, string errorMessage)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                return await operation();
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = errorMessage;
                logger.LogError(ex, errorMessage);
                return operationResult;
            }
        }

        public override async Task<OperationResult> Save(Status entity)
        {
            OperationResult operationResult = ValidateStatusEntity(entity);

            if (!operationResult.Success)
            {
                return operationResult;
            }

            if (await base.Exists(status => status.StatusName == entity.StatusName))
            {
                return new OperationResult
                {
                    Success = false,
                    Message = "Este estado ya se encuentra registrado."
                };

            }
            return await ExecuteOperationWithLogging(() => base.Save(entity), "Error guardando el estado.");
        }
        public async override Task<OperationResult> Update(Status entity)
        {
            OperationResult operationResult = ValidateStatusEntity(entity);

            if (!operationResult.Success)
            {
                return operationResult;
            }

            return await ExecuteOperationWithLogging(async () =>
            {
                Status? statusToUpdate = await _medicalAppointmentContext.Status.FindAsync(entity.StatusID);
                if (statusToUpdate == null)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "El estado no existe."
                    };
                }
                statusToUpdate.StatusName = entity.StatusName;

                return await base.Update(statusToUpdate);
            }, "Error actualizando el estado.");
        }
        public async override Task<OperationResult> Remove(Status entity)
        {
            OperationResult operationResult = ValidateStatusEntity(entity);

            if (!operationResult.Success) { return operationResult; }

            return await ExecuteOperationWithLogging(async () =>
            {
                Status? statusToRemove = await _medicalAppointmentContext.Status.FindAsync(entity.StatusID);

                if (statusToRemove == null)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "El estado no existe."
                    };
                }
                _medicalAppointmentContext.Status.Remove(statusToRemove);
                await _medicalAppointmentContext.SaveChangesAsync();

                return new OperationResult { Success = true };
            }, "Error eliminando el estado.");

        }
        public override async Task<OperationResult> GetAll()
        {
            return await ExecuteOperationWithLogging(async () =>
            {
                var statuses = await _medicalAppointmentContext.Status.ToListAsync();

                return new OperationResult
                {
                    Success = true,
                    Data = statuses
                };
            }, "Error obteniendo los estados.");
        }
        public override async Task<OperationResult> GetEntityBy(int Id)
        {
            return await ExecuteOperationWithLogging(async () =>
            {
                var status = await _medicalAppointmentContext.Status.FindAsync(Id);

                if (status == null)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "El estado no fue encontrado."
                    };
                }

                return new OperationResult
                {
                    Success = true,
                    Data = status
                };
            }, "Error obteniendo el estado.");
        }
    }
}
