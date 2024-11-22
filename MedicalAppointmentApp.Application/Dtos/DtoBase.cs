
namespace MedicalAppointmentApp.Application.Dtos
{
    public class DtoBase
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
