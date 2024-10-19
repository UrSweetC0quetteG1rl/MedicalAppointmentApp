

namespace MedicalAppointment.Persistance.Models
{
    public class AppointmentDoctorModel
    {
        public int AppointmentID { get; set; }
        public int DoctorID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime CreatedAt { get; set; }       
        public int PatientID { get; set; }
        
        

    }
}
