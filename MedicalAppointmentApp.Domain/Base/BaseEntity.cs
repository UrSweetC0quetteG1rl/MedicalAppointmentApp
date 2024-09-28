
namespace MedicalAppointmentApp.Domain.Base
{
    //Naomi Meran #2023-1514
    public abstract class BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
