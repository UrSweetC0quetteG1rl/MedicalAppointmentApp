using MedicalAppointmentApp.Domain.Base;

namespace MedicalAppointmentApp.Domain.Entities.Appoinments
{
    //Orlando Martinez 2020-10382
    public class Appointments : BaseEntity
    {
        public int AppointmentsID { get; set; }

        public int PatientID { get; set; }

        public int DoctorID { get; set; }

        public DateTime AppointmentDate { get; set; }

        public int StatusID { get; set; }


    }
}
