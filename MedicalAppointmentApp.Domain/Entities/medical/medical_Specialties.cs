using MedicalAppointmentApp.Domain.Base;

namespace MedicalAppointmentApp.Domain.Entities.medical
{
    internal class Medical_Specialties : BaseEntity
    {
        public short SpecialtyID { get; set; }

        public string SpecialtyName { get; set; }

    }
}
