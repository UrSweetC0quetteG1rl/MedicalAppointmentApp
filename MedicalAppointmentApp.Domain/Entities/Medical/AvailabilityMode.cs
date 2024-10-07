
using MedicalAppointmentApp.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAppointmentApp.Domain.Entities.Medical
{
    [Table("AvailabilityModes", Schema = "dbo")]
    //Orlando Martinez 2020-10382
    public class AvailabilityMode: BaseEntity
    {
        [Key]
        public short SAvailabilityModeID { get; set; }

        public string Availability_Mode { get; set; }
    }
}
