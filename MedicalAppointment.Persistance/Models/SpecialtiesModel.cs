

namespace MedicalAppointment.Persistance.Models
{
    public sealed class SpecialtiesModel
    {
        public int SpecialtyID { get; set; }

        public string SpecialtyName { get; set;}
        

        public DateTime? UpdatedAt { get; set; }


        public bool IsActive { get; set; }


    }
}
