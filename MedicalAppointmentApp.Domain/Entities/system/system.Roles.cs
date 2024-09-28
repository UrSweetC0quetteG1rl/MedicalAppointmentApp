using MedicalAppointmentApp.Domain.Base;

namespace MedicalAppointmentApp.Domain.Entities.system
{
    public class System_Roles : BaseEntity
    { 
        public int RoleID {get; set; }
        public string RoleName {get; set; }
    }
}
