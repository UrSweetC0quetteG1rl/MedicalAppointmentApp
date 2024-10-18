

namespace MedicalAppointment.Persistance.Models.User
{
    public sealed class PatientInsurenceProviderModel
    {
        public int PatientID { get; set; }
        public DateTime DateOfBirth { get; set; }
        public char Gender { get; set; }
        public string Address { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; }
        public string BloodType { get; set; }
        public string Allergies { get; set; }
        public int InsuranceProviderID { get; set; }
        public string? InsuranceProviderName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
