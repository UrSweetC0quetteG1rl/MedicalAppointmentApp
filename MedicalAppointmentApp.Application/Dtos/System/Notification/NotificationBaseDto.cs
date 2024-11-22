

namespace MedicalAppointmentApp.Application.Dtos.System.Notification
{
    public class NotificationBaseDto
    {
        public int NotificationID { get; set; }
        public int UserID { get; set; }
        public string Message { get; set; }
        public DateTime? SentAt { get; set; }
    }
}
