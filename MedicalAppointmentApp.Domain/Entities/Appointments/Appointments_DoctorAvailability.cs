

namespace MedicalAppointmentApp.Domain.Entities.Appoinments
{
    internal class Appointments_DoctorAvailability
    {
        public int Availability { get; set; }

        public int DoctorID { get; set; }

        public DateTime AvaileDate { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }


    }
}
