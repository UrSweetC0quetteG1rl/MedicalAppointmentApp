

namespace MedicalAppointmentApp.Application.Dtos.Users.Patient
{
    public class PatientBaseDto: DtoBase
    {
        public int PatientID { get; set; }
        public DateTime DateOfBirth { get; set; }
        public char Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; }
        public string BloodType { get; set; }
        public string Allergies { get; set; }
        public int InsuranceProviderID { get; set; }
    }
}
