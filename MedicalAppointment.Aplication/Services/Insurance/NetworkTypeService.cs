using MedicalAppointment.Aplication.Contracts.Insurance;
using MedicalAppointment.Aplication.Dtos.Configuration.Insurnace.NetworkType;
using MedicalAppointment.Aplication.Responses.Configuration.NetworkType;
using MedicalAppointment.Persistance.Interfaces.Configuration.Insurance;
using MedicalAppointmentApp.Domain.Entities.Insurance;
using Microsoft.Extensions.Logging;


namespace MedicalAppointment.Aplication.Services.Insurance
{
    public class NetworkTypeService : INetworkType
    {

        private readonly ILogger _logger;
        private readonly INetworkTypeRepository _networkType;


        public NetworkTypeService(INetworkTypeRepository networkType,
            ILogger<NetworkTypeService> logger)
        {

            if (networkType is null)
            {
                throw new ArgumentNullException(nameof(networkType));

            }

            _logger = logger;
            _networkType = networkType; 


        }

        public async Task<NetworkTypeResponse> GetAll()
        {
            
            NetworkTypeResponse networkType = new NetworkTypeResponse();


            try
            {

                var result = await _networkType.GetAll();

                if (!result.Success)
                {
                    networkType.IsSuccess = result.Success;
                    networkType.Message = result.Message;
                    return networkType;


                }
                networkType.Data = result.Data;

            }
            catch (Exception ex)
            {

                networkType.IsSuccess = false;
                networkType.Message = "error obteniendo los network";
                _logger.LogError(networkType.Message, ex);


            }
            return networkType;

        }

        public async Task<NetworkTypeResponse> GetById(int Id)
        {
            NetworkTypeResponse networkType = new NetworkTypeResponse();

            try
            {
                var result = await _networkType.GetEntityBy(Id);

                if (!result.Success)
                {
                    networkType.IsSuccess = result.Success;
                    networkType.Message = result.Message;
                    return networkType;
                }
                networkType.Data = result.Data;

            }
            catch (Exception ex)
            {

                networkType.IsSuccess = false;
                networkType.Message = "error obteniendo los network";
                _logger.LogError(networkType.Message, ex);


            }
            return networkType;


        }

        public async Task<NetworkTypeResponse> SaveAsync(NetworkSaveDto dto)
        {
            NetworkTypeResponse networkType = new NetworkTypeResponse();

            try
            {
                NetworkType network = new NetworkType();

                network.Name = dto.Name;
                network.Description = dto.Description;
                network.CreatedAt = dto.CreatedAt;
                network.UpdatedAt = dto.UpdatedAt;
                network.IsActive = dto.IsActive;

                var result = await _networkType.Save(network);
                result.Message = "Dato guardado";

            }
            catch (Exception ex)
            {

                networkType.IsSuccess = false;
                networkType.Message = "error obteniendo los network";
                _logger.LogError(networkType.Message, ex);


            }
            return networkType;
        }

        public async Task<NetworkTypeResponse> UpdateAsync(NetworkUpdateDto dto)
        {
            NetworkTypeResponse networkType = new NetworkTypeResponse();

            try
            {
                var resultGetBy = await _networkType.GetEntityBy(dto.NetworkTypeId);

                if(!resultGetBy.Success)
                {
                    networkType.IsSuccess = false;
                    networkType.Message = "No se encontro";
                    return networkType;
                }


                NetworkType networkToupdate = new NetworkType();

                networkToupdate.NetworkTypeId = dto.NetworkTypeId;
                networkToupdate.Name = dto.Name;
                networkToupdate.Description = dto.Description;               
                networkToupdate.UpdatedAt = dto.UpdatedAt;
                networkToupdate.IsActive = dto.IsActive;

                var result = await _networkType.Update(networkToupdate);
                result.Message = "Actualizado";
            }
            catch (Exception ex)
            {

                networkType.IsSuccess = false;
                networkType.Message = "error obteniendo los network";
                _logger.LogError(networkType.Message, ex);


            }
            return networkType;

        }
    }
}
