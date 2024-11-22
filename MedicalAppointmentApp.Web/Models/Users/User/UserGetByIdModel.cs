using MedicalAppointment.Persistance.Models.User;

namespace MedicalAppointmentApp.Web.Models.Users.Users
{
    public class UserGetByIdModel : BaseApiResponseModel
    {
        public UserRoleModel data { get; set; }
    }
}
