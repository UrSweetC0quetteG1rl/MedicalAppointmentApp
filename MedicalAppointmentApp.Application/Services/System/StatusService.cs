

using MedicalAppointment.Persistance.Interfaces.System;
using MedicalAppointmentApp.Application.Contracts.System;
using MedicalAppointmentApp.Application.Dtos.System.Status;
using MedicalAppointmentApp.Application.Responses.System.Status;
using MedicalAppointmentApp.Domain.Entities.System;
using Microsoft.Extensions.Logging;

namespace MedicalAppointmentApp.Application.Services.System
{
    
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;
        private readonly ILogger<StatusService> _logger;

        public StatusService(IStatusRepository statusRepository, ILogger<StatusService> logger)
        {
            if (statusRepository is null) { throw new ArgumentNullException(nameof(statusRepository)); }

            this._statusRepository = statusRepository;
            this._logger = logger;
        }

        public async Task<StatusResponse> GetAll()
        {
            StatusResponse statusResponse = new StatusResponse();

            try
            {
                var result = await _statusRepository.GetAll();

                if (!result.Success)
                {
                    statusResponse.Message = result.Message;
                    statusResponse.IsSuccess = result.Success;
                    return statusResponse;
                }
                statusResponse.Data = result.Data;
            }
            catch (Exception ex)
            {
                statusResponse.IsSuccess = false;
                statusResponse.Message = "Error obteninedo los estados";
                _logger.LogError(statusResponse.Message, ex.ToString());
            }
            return statusResponse;  
        }

        public async Task<StatusResponse> GetById(int Id)
        {
            StatusResponse statusResponse = new StatusResponse();

            try
            {
                var result = await _statusRepository.GetEntityBy(Id);

                if (!result.Success)
                {
                    statusResponse.Message = result.Message;
                    statusResponse.IsSuccess = result.Success;
                    return statusResponse;
                }
                statusResponse.Data = result.Data;
            }
            catch (Exception ex)
            {
                statusResponse.IsSuccess = false;
                statusResponse.Message = "Error obteninedo el status";
                _logger.LogError(statusResponse.Message, ex.ToString());
            }
            return statusResponse;
        }

        public async Task<StatusResponse> SaveAsync(StatusDtoSave dto)
        {

            StatusResponse statusResponse = new StatusResponse();

            try
            {
                Status status = new Status();

                status.StatusName = dto.StatusName;

            }
            catch (Exception ex)
            {
                statusResponse.IsSuccess = false;
                statusResponse.Message = "Error guardando el status";
                _logger.LogError(statusResponse.Message, ex.ToString());
            }
            return statusResponse;
        }

        public async Task<StatusResponse> UpdateAsync(StatusDtoUpdate dto)
        {

            StatusResponse statusResponse = new StatusResponse();

            try
            {
                var resultGet = await _statusRepository.GetEntityBy(dto.StatusID);

                if (!resultGet.Success)
                {
                    statusResponse.IsSuccess = resultGet.Success;
                    statusResponse.Message = resultGet.Message;
                    return statusResponse;
                }

                Status status = (Status)resultGet.Data;

                status.StatusID = dto.StatusID;
                status.StatusName = dto.StatusName;
            }
            catch (Exception ex)
            {
                statusResponse.IsSuccess = false;
                statusResponse.Message = "Error actualizando el Status";
                _logger.LogError(statusResponse.Message, ex.ToString());
            }
            return statusResponse;
        }
    }
}
