
using MedicalAppointmentApp.Domain.Base;

namespace MedicalAppointmentApp.Domain.Entities.Medical
{
    //Orlando Martinez 2020-10382
    public class Medical_AvailabilityModes: BaseEntity
    {
        public short SAvailability { get; set; }

        public string AvailabilityMobe { get; set; }
    }
}
