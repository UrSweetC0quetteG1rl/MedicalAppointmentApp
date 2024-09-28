

namespace MedicalAppointmentApp.Domain.Entities.system
{
    public class System_Notifications
    {
        public int NotificationID { get; set; }
        public int UserID { get; set; }
        public string Message { get; set; }
        public DateTime? SentAt { get; set; }

    }
}
