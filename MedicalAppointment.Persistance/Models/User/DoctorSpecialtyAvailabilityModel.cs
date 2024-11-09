
namespace MedicalAppointment.Persistance.Models.User
{
    public sealed class DoctorSpecialtyAvailabilityModel
    {
        public int DoctorID { get; set; }
        public int SpecialtyID { get; set; }
        public string SpecialtyName { get; set; }
        public string? Bio { get; set; }
        public decimal? ConsultationFee { get; set; }
        public string? ClinicAddress { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
