using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Domain.Entities
{
    internal class appointments_DoctorAvailability
    {
        public int Availability {  get; set; }

        public int DoctorID { get; set; }

        public DateTime AvaileDate { get; set; }

        public DateTime StartTime  { get; set; }

        public DateTime EndTime { get; set; }


    }
}
