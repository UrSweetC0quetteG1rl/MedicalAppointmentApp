


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAppointmentApp.Domain.Entities.Appoinments
{
    //Orlando Martinez 2020-10382 
    [Table("DoctorAvailability", Schema = "appointments")]
    public class DoctorAvailability
    {
        [Key]
        public int AvailabilityID { get; set; }

        public int DoctorID { get; set; }

        public DateTime AvailableDate { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }


    }
}
