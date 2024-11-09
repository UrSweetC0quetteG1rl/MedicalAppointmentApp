

namespace MedicalAppointmentApp.Application.Dtos.Users.User
{
    public class UserBaseDto: DtoBase
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
