
namespace MedicalAppointment.Persistance.Models.User
{
    public sealed class UserModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? RoleID { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
