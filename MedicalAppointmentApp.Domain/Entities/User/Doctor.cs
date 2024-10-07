using MedicalAppointmentApp.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MedicalAppointmentApp.Domain.Entities.User
{
    [Table("Doctors", Schema = "dbo")]
    public sealed class Doctor : BaseEntity
    {
        [Key]
        public int DoctorID { get; set; }
        public int SpecialtyID { get; set; }
        public string LicenseNumber { get; set; }
        public int YearsOfExperience { get; set; }
        public string? Bio {  get; set; }
        public decimal? ConsultationFee { get; set; }
        public string? ClinicAddress { get; set; }
        public int? AvailabilityModeld {  get; set; }
        public DateTime LicenseExpirationDate { get; set; }

    }
}
