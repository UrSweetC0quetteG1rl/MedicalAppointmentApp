

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAppointmentApp.Domain.Entities.Appoinments
{
    //Orlando Martinez 2020-10382 
    [Table("DoctorAvailability", Schema = "dbo")]
    public class DoctorAvailability
    {
        [Key]
        public int Availability { get; set; }

        public int DoctorID { get; set; }

        public DateTime AvaileDate { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }


    }
}
