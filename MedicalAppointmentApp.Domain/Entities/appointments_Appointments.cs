using MedicalAppointmentApp.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Domain.Entities
{
    public class appointments_appointments : BaseEntity
    {
        public int AppoinmentsID { get; set; }

        public int PatientID { get; set; }

        public int DoctorID { get; set; }

        public DateTime AppoimentDate { get; set; }

        public int StatusID { get; set; }


    }
}
