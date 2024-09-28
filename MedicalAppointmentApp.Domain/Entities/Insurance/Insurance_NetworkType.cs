using MedicalAppointmentApp.Domain.Base;

namespace MedicalAppointmentApp.Domain.Entities.insurance
{
    //Orlando Martinez 2020-10382
    public class Insurance_NetworkType : BaseEntity
    {
        public int NetworkTypeID { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

    }
}
