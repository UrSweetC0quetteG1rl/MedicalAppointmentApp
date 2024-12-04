using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointment.Aplication.Dtos.Configuration.Appointment.DoctorAvailability
{
    public class DoctorAvailabilityBaseDto 
    {
        public int DoctorID { get; set; }

        public DateTime AvailableDate { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

    }
}
