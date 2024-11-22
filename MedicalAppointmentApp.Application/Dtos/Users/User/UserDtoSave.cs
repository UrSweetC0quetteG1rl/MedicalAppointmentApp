

namespace MedicalAppointmentApp.Application.Dtos.Users.User
{
    public class UserDtoSave: UserBaseDto
    {

        public string Password { get; set; }
        public int? RoleID { get; set; }

    }
}
