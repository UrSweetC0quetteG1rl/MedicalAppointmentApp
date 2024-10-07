using MedicalAppointmentApp.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MedicalAppointmentApp.Domain.Entities.Insurance
{
    [Table("NetworkType", Schema = "dbo")]
    //Orlando Martinez 2020-10382
    public class NetworkType: BaseEntity
    {
        [Key]
        public int NetworkTypeID { get; set; }
        public string nombre { get; set; }
        public string? Description { get; set; }

    }
}
