using MedicalAppointment.Persistance.Models.User;

namespace MedicalAppointmentApp.Web.Models.Users.Users
{
    public class UserGetAllResultModel : BaseApiResponseModel
    {
        public List<UserRoleModel> data { get; set; }
    }
}
