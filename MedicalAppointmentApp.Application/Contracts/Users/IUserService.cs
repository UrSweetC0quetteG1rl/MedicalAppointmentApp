


using MedicalAppointmentApp.Application.Base;
using MedicalAppointmentApp.Application.Dtos.Users.User;
using MedicalAppointmentApp.Application.Responses.Users.User;

namespace MedicalAppointmentApp.Application.Contracts.Users
{
    public interface IUserService : IBaseService<UserResponse, UserDtoSave, UserDtoUpdate>
    {
    }
}
