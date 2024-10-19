
namespace MedicalAppointment.Persistance.Models
{
    public sealed class AvailabilityModesModel
    {

        public int SAvailabilityModeID { get; set; }

        public string AvailabilityMode { get; set; }


        public DateTime? UpdatedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsActive { get; set; }



    }
}