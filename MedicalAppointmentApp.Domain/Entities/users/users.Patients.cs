using MedicalAppointmentApp.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;


namespace MedicalAppointmentApp.Domain.Entities.users
{
    public sealed class Users_Patients : BaseEntity
    {
        public int PatientID { get; set; }
        public DateTime DateOfBirth { get; set; }
        public char Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Adsress { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; }
        public string BloodType { get; set; }
        public string Allergies { get; set; }
        public int InsuranceProviderID { get; set; }

    }
}
