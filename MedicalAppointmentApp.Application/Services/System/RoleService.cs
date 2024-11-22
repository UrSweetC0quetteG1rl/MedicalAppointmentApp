

using MedicalAppointment.Persistance.Interfaces.System;
using MedicalAppointmentApp.Application.Contracts.System;
using MedicalAppointmentApp.Application.Dtos.System.Role;
using MedicalAppointmentApp.Application.Responses.System.Role;
using MedicalAppointmentApp.Domain.Entities.System;
using Microsoft.Extensions.Logging;

namespace MedicalAppointmentApp.Application.Services.System
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger<RoleService> _logger;

        public RoleService(IRoleRepository roleRepository, ILogger<RoleService> logger)
        {
            if (roleRepository is null) { throw new ArgumentNullException(nameof(roleRepository)); }
            _roleRepository = roleRepository;
            _logger = logger;
        }

        public async Task<RoleResponse> GetAll()
        {
            RoleResponse roleResponse = new RoleResponse();

            try
            {
                var result = await _roleRepository.GetAll();

                if (!result.Success)
                {
                    roleResponse.Message = result.Message;
                    roleResponse.IsSuccess = result.Success;
                    return roleResponse;
                }
                roleResponse.Data = result.Data;
            }
            catch (Exception ex)
            {
                roleResponse.IsSuccess = false;
                roleResponse.Message = "Error obteninedo los roles";
                _logger.LogError(roleResponse.Message, ex.ToString());
            }
            return roleResponse;
        }

        public async Task<RoleResponse> GetById(int Id)
        {
            RoleResponse roleResponse = new RoleResponse();

            try
            {
                var result = await _roleRepository.GetEntityBy(Id);

                if (!result.Success)
                {
                    roleResponse.Message = result.Message;
                    roleResponse.IsSuccess = result.Success;
                    return roleResponse;
                }
                roleResponse.Data = result.Data;
            }
            catch (Exception ex)
            {
                roleResponse.IsSuccess = false;
                roleResponse.Message = "Error obteninedo el rol";
                _logger.LogError(roleResponse.Message, ex.ToString());
            }
            return roleResponse;
        }

        public async Task<RoleResponse> SaveAsync(RoleDtoSave dto)
        {

            RoleResponse roleResponse = new RoleResponse();

            try
            {
                Role role = new Role();

                role.RoleName = dto.RoleName;
                role.IsActive = dto.IsActive;
                role.CreatedAt = dto.CreatedAt;

            }
            catch (Exception ex)
            {
                roleResponse.IsSuccess = false;
                roleResponse.Message = "Error guardando el rol";
                _logger.LogError(roleResponse.Message, ex.ToString());
            }
            return roleResponse;
        }

        public async Task<RoleResponse> UpdateAsync(RoleDtoUpdate dto)
        {
            RoleResponse roleResponse = new RoleResponse();

            try
            {
                var resultGet = await _roleRepository.GetEntityBy(dto.RoleID);

                if (!resultGet.Success)
                {
                    roleResponse.IsSuccess = resultGet.Success;
                    roleResponse.Message = resultGet.Message;
                    return roleResponse;
                }

               Role role = (Role)resultGet.Data;

                role.RoleName = dto.RoleName;
                role.IsActive = dto.IsActive;
                role.UpdatedAt = dto.UpdatedAt;
            }
            catch (Exception ex)
            {
                roleResponse.IsSuccess = false;
                roleResponse.Message = "Error actualizando el Status";
                _logger.LogError(roleResponse.Message, ex.ToString());
            }
            return roleResponse;
        }
    }
}
