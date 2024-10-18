
namespace MedicalAppointment.Persistance.Models.System
{
    public sealed class RolModel
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
