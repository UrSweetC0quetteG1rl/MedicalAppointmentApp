using MedicalAppointmentApp.Domain.Base;

namespace MedicalAppointmentApp.Domain.Entities.insurance
{
    internal class Insurance_NetworkType : BaseEntity
    {
        public int NetworkTypeID { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

    }
}
