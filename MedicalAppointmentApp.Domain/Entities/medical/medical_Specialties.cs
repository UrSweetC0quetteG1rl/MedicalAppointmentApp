using MedicalAppointmentApp.Domain.Base;

namespace MedicalAppointmentApp.Domain.Entities.medical
{
    //Orlando Martinez 2020-10382
    public class Medical_Specialties : BaseEntity
    {
        public short SpecialtyID { get; set; }

        public string SpecialtyName { get; set; }

    }
}
