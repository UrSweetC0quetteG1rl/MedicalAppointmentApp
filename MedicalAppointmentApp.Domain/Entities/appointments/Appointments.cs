using MedicalAppointmentApp.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAppointmentApp.Domain.Entities.Appoinments
{
    [Table("Appointments", Schema = "appointments")]
    public class Appointments 
    {
        [Key]
        public int AppointmentID { get; set; }

        public int PatientID { get; set; }

        public int DoctorID { get; set; }

        public DateTime AppointmentDate { get; set; }

        public int StatusID { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
