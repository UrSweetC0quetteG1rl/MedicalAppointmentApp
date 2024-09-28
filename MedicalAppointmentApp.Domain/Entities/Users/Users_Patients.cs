//Naomi Meran 2023-1514

namespace MedicalAppointmentApp.Domain.Entities.Users
{
    public sealed class Users_Patients : Users_Users
    {
        public int PatientID { get; set; }
        public DateTime DateOfBirth { get; set; }
        public char Gender { get; set; }
        public string Adsress { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; }
        public string BloodType { get; set; }
        public string Allergies { get; set; }
        public int InsuranceProviderID { get; set; }

    }
}
