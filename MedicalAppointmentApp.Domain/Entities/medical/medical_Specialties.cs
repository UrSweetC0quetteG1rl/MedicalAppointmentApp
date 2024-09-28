using MedicalAppointmentApp.Domain.Base;

namespace MedicalAppointmentApp.Domain.Entities.Medical
{
    //Orlando Martinez 2020-10382
    public class Medical_Specialties: BaseEntity
    {
        public short SpecialtyID { get; set; }

        public string SpecialtyName { get; set; }
    }
}
