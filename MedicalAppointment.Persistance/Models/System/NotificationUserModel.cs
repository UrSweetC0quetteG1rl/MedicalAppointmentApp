
namespace MedicalAppointment.Persistance.Models.System
{
    public sealed class NotificationUserModel
    {
        public int NotificationID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public DateTime? SentAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
