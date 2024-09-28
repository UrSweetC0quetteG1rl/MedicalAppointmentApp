﻿using MedicalAppointmentApp.Domain.Base;

namespace MedicalAppointmentApp.Domain.Entities.medical
{
    internal class Medical_MedicalRecords : BaseEntity
    {
        public int RecordID { get; set; }

        public int PatientID { get; set; }

        public int DoctorID { get; set; }

        public string Diagnosis { get; set; }

        public string Treatment { get; set; }

        public DateTime DateOfVisit { get; set; }


    }
}
