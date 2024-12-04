using MedicalAppointment.Aplication.Base;
using MedicalAppointment.Aplication.Dtos.Configuration.Insurnace.NetworkType;
using MedicalAppointment.Aplication.Responses.Configuration.NetworkType;

namespace MedicalAppointment.Aplication.Contracts.Insurance
{
    public interface INetworkType : IBaseService<NetworkTypeResponse, NetworkSaveDto, NetworkUpdateDto>
    {
    }
}
