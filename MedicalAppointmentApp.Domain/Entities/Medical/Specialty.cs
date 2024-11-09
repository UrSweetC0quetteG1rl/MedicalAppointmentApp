using MedicalAppointmentApp.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAppointmentApp.Domain.Entities.Medical
{
    [Table("Specialties", Schema = "medical")]
    //Orlando Martinez 2020-10382
    public class Specialty: BaseEntity
    {
        [Key]
        public short SpecialtyID { get; set; }

        public string SpecialtyName { get; set; }
    }
}
