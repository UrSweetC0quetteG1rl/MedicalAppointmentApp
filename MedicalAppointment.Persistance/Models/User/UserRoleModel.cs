
namespace MedicalAppointment.Persistance.Models.User
{
    public sealed class UserRoleModel
    {
        public int UserId { get; set; }
        public int? RoleID { get; set; }
        public string? Role {  get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }

    }
}
