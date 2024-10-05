using MedicalAppointmentApp.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;


namespace MedicalAppointmentApp.Domain.Entities.Users
{
    internal sealed class Users_Doctors : Users_Users
    {
        public int DoctorID { get; set; }
        public int SpecialtyID { get; set; }
        public string LicenseNumber { get; set; }
        public int YearsOfExperience { get; set; }
        public string? Bio {  get; set; }
        public decimal? ConsultationFee { get; set; }
        public string? ClinicAddress { get; set; }
        public int? AvailabilityModeld {  get; set; }
        public DateTime LicenseExpirationDate { get; set; }

    }
}
