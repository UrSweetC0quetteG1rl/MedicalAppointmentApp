using MedicalAppointmentApp.Application.Base;
using MedicalAppointmentApp.Application.Dtos.System.Role;
using MedicalAppointmentApp.Application.Responses.System.Role;

namespace MedicalAppointmentApp.Application.Contracts.System
{
    public interface IRoleService : IBaseService<RoleResponse, RoleDtoSave, RoleDtoUpdate>
    {
    }
}
