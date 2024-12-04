namespace MedicalAppointment.Persistance.Models.Insurnaces
{
    public sealed class NetworkTypeModel
    {

        public int NetworkTypeId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public bool IsActive { get; set; }


    }
}
