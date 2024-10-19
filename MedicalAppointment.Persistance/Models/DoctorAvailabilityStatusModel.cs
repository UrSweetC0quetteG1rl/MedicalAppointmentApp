

namespace MedicalAppointment.Persistance.Models
{
    public class DoctorAvailabilityStatusModel
    {

        public int AvailabilityID { get; set; }
        public int DoctorID { get; set; }
        public DateTime AvailaDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsActive { get; set; }

    }
}
