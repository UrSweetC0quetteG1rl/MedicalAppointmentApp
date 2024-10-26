using MedicalAppointment.Persistance.Interfaces.System;
using MedicalAppointmentApp.Domain.Entities.System;
using MedicalAppointmentApp.System.Api.Base;
using Microsoft.AspNetCore.Mvc;


namespace MedicalAppointmentApp.System.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : BaseController
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationController(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        [HttpGet("GetNotifications")]
        public async Task<IActionResult> Get()
        {
            return await HandleRepositoryAction(async () => await _notificationRepository.GetAll());
        }


        [HttpGet("GetNotificationById")]
        public async Task<IActionResult> Get(int id)
        {
            return await HandleRepositoryAction(async () => await _notificationRepository.GetEntityBy(id));
        }


        [HttpPost("SaveNotification")]
        public async Task<IActionResult> Post([FromBody] Notification notification)
        {
            return await HandleRepositoryAction(async () => await _notificationRepository.Save(notification));
        }


        [HttpPut("UpdateNotification")]
        public async Task<IActionResult> Put([FromBody] Notification notification)
        {
            return await HandleRepositoryAction(async () => await _notificationRepository.Update(notification));
        }


        [HttpDelete("DeleteNotification")]
        public async Task<IActionResult> Delete(Notification notification)
        {

            return await HandleRepositoryAction(async () => await _notificationRepository.Remove(notification));
        }
    }
}
