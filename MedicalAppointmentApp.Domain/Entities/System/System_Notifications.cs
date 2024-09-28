//Naomi Meran 2023-1514

namespace MedicalAppointmentApp.Domain.Entities.System
{
    public class System_Notifications
    {
        public int NotificationID { get; set; }
        public int UserID { get; set; }
        public string Message { get; set; }
        public DateTime? SentAt { get; set; }

    }
}
