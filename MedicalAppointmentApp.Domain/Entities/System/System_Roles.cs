using MedicalAppointmentApp.Domain.Base;

//Naomi Meran 2023-1514
namespace MedicalAppointmentApp.Domain.Entities.System
{
    public class System_Roles : BaseEntity
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
    }
}
