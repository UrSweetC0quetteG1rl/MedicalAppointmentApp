using MedicalAppointmentApp.Domain.Base;

namespace MedicalAppointmentApp.Domain.Entities.Users
{
    public class Users_Users : BaseEntity
    {
        public int UserId {  get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleID { get; set; }
    }
}
