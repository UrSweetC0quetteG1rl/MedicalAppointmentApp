

using MedicalAppointmentApp.Application.Core;

namespace MedicalAppointmentApp.Application.Responses.Users.User
{
    public sealed class UserResponse: BaseResponse 
    {
        public dynamic? Data { get; set; }
    }
}
