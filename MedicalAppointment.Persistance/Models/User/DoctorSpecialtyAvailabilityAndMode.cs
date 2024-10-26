using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointment.Persistance.Models.User
{
    public sealed class DoctorSpecialtyAvailabilityAndMode
    {
        public int DoctorID { get; set; }
        public int SpecialtyID { get; set; }
        public string SpecialtyName { get; set; }
        public string LicenseNumber { get; set; }
        public int YearsOfExperience { get; set; }
        public string? Bio { get; set; }
        public decimal? ConsultationFee { get; set; }
        public string? ClinicAddress { get; set; }
        public int? AvailabilityModelId { get; set; }
        public string AvailabilityModelName { get; set; }
        public DateTime LicenseExpirationDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public DateTime AvaileDate { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

    }
}
