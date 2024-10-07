using MedicalAppointmentApp.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAppointmentApp.Domain.Entities.Appoinments
{
    [Table("Appointments", Schema = "dbo")]
    public class Appointment : BaseEntity
    {
        [Key]
        public int AppointmentsID { get; set; }

        public int PatientID { get; set; }

        public int DoctorID { get; set; }

        public DateTime AppointmentDate { get; set; }

        public int StatusID { get; set; }


    }
}
