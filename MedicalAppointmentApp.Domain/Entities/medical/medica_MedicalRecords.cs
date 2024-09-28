using MedicalAppointmentApp.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Domain.Entities.medical
{
    internal class medica_MedicalRecords : BaseEntity
    {
        public int RecordID { get; set; }

        public int PatientID { get; set; }

        public int DoctorID { get; set; }

        public string Diagnosis { get; set; }

        public string Treatment { get; set; }

        public DateTime DateOfVisit { get; set; }


    }
}
