using MedicalAppointmentApp.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAppointmentApp.Domain.Entities.User
{
    [Table("Patients", Schema = "dbo")]
    public sealed class Patient : BaseEntity
    {
        [Key]
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
