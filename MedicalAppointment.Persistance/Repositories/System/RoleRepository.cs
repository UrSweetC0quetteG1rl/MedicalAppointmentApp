using MedicalAppointment.Persistance.Base;
using MedicalAppointment.Persistance.Context;
using MedicalAppointment.Persistance.Interfaces.System;
using MedicalAppointmentApp.Domain.Entities.System;
using MedicalAppointmentApp.Domain.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedicalAppointment.Persistance.Repositories.System
{
    public sealed class RoleRepository: BaseRepository<Role>, IRoleRepository
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private readonly ILogger<RoleRepository> _logger;

        public RoleRepository(MedicalAppointmentContext medicalAppointmentContext, ILogger<RoleRepository> logger)
        : base(medicalAppointmentContext)
        {
            _medicalAppointmentContext = medicalAppointmentContext;
            _logger = logger;
        }

        private OperationResult ValidateRoleEntity(Role entity)
        {
            OperationResult result = new OperationResult();

            if (entity == null)
            {
                result.Success = false;
                result.Message = "La entidad es requerida.";
                return result;
            }

            if (string.IsNullOrEmpty(entity.RoleName))
            {
                result.Success = false;
                result.Message = "El nombre del rol es necesario.";
                return result;
            }


            result.Success = true;
            return result;
        }
        private async Task<OperationResult> ExecuteOperationWithLogging(Func<Task<OperationResult>> operation, string errorMessage)
        {
            var operationResult = new OperationResult();
            try
            {
                return await operation();
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = errorMessage;
                _logger.LogError(ex, errorMessage);
                return operationResult;
            }
        }

        public override async Task<OperationResult> Save(Role entity)
        {
            OperationResult operationResult = ValidateRoleEntity(entity);

            if (!operationResult.Success)
            {
                return operationResult;
            }

            if (await base.Exists(role => role.RoleName == entity.RoleName))
            {
                return new OperationResult
                {
                    Success = false,
                    Message = "Este rol ya se encuentra registrado."
                };

            }
            return await ExecuteOperationWithLogging(() => base.Save(entity), "Error guardando el rol.");
        }
        public async override Task<OperationResult> Update(Role entity)
        {
            OperationResult operationResult = ValidateRoleEntity(entity);

            if (!operationResult.Success)
            {
                return operationResult;
            }

            return await ExecuteOperationWithLogging(async () =>
                {
                    Role? roleToUpdate = await _medicalAppointmentContext.Roles.FindAsync(entity.RoleID);
                    if (roleToUpdate == null)
                    {
                        return new OperationResult
                        {
                            Success = false,
                            Message = "El rol no existe."
                        };
                    }
                    
                    roleToUpdate.RoleName = entity.RoleName;
                    roleToUpdate.UpdatedAt = entity.UpdatedAt; //o DateTime.Now ya que se acaba de actualizar

                    return await base.Update(roleToUpdate);
                }, "Error actualizando el rol.");
        }
        public async override Task<OperationResult> Remove(Role entity)
        {
            OperationResult operationResult = ValidateRoleEntity(entity);

            if (!operationResult.Success) { return operationResult; }

            return await ExecuteOperationWithLogging(async () =>
            {
                Role? roleToRemove = await _medicalAppointmentContext.Roles.FindAsync(entity.RoleID);

                if (roleToRemove == null)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "El rol no existe."
                    };
                }
                roleToRemove.IsActive = false;
                roleToRemove.UpdatedAt = entity.UpdatedAt;

                return await base.Update(roleToRemove);
            }, "Error desactivando el rol.");
        }
        public async override Task<OperationResult> GetAll()
        {
            return await ExecuteOperationWithLogging(async () =>
            {
                var roles = await _medicalAppointmentContext.Roles
                    .Where(role => role.IsActive)
                    .OrderByDescending(role => role.CreatedAt)
                    .ToListAsync();

                return new OperationResult
                {
                    Success = true,
                    Data = roles
                };
            }, "Error obteniendo los roles.");
        }
        public async override Task<OperationResult> GetEntityBy(int Id)
        {
            return await ExecuteOperationWithLogging(async () =>
            {
                var role = await _medicalAppointmentContext.Roles
                    .Where(role => role.IsActive && role.RoleID == Id)
                    .FirstOrDefaultAsync();

                if (role == null)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "El rol no fue encontrado."
                    };
                }

                return new OperationResult
                {
                    Success = true,
                    Data = role
                };
            }, "Error obteniendo el rol.");
        }


    }
}
