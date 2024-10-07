using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAppointmentApp.Domain.Entities.System
{
    [Table("Status", Schema = "dbo")]
    public class Status
    {
        [Key]
        public int StatusID { get; set; }
        public string StatusName { get; set; }
    }
}
