


namespace MedicalAppointment.Aplication.Dtos.Configuration.Appointment.Appointments
{
    public class AppointmentBaseDto : DtoBase
    {
        

        public int PatientID { get; set; }

        public int DoctorID { get; set; }

        public DateTime AppointmentDate { get; set; }

        public int StatusID { get; set; }

    }
}
