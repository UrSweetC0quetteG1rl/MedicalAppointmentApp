

namespace MedicalAppointmentApp.Application.Dtos.Users.User
{
    public class UserDtoUpdate : UserBaseDto
    {

        public string Password { get; set; }
        public int? RoleID { get; set; }
        public bool IsActive { get; set; }
    }
}
