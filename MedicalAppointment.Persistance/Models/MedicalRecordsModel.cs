

namespace MedicalAppointment.Persistance.Models
{
    public class MedicalRecordsModel
    {
        public int RecordID { get; set; }
        public int DoctorID { get; set; }

        public string Diagnosis { get; set; }

        public string Treatment { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime DateOfVisit { get; set; }

        public int PatientID { get; set; }
    }
}
