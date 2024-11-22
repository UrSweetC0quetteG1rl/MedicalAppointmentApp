
using MedicalAppointmentApp.Application.Contracts.System;
using MedicalAppointmentApp.Application.Dtos.System.Notification;
using MedicalAppointmentApp.System.Api.Base;
using Microsoft.AspNetCore.Mvc;


namespace MedicalAppointmentApp.System.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : BaseController
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("GetNotifications")]
        public async Task<IActionResult> Get()
        {
            return await HandleRepositoryAction(async () => await _notificationService.GetAll());
        }


        [HttpGet("GetNotificationById")]
        public async Task<IActionResult> Get(int id)
        {
            return await HandleRepositoryAction(async () => await _notificationService.GetById(id));
        }


        [HttpPost("SaveNotification")]
        public async Task<IActionResult> Post([FromBody] NotificationDtoSave notificationDtoSave)
        {
            return await HandleRepositoryAction(async () => await _notificationService.SaveAsync(notificationDtoSave));
        }


        [HttpPut("UpdateNotification")]
        public async Task<IActionResult> Put([FromBody] NotificationDtoUpdate notificationDtoUpdate)
        {
            return await HandleRepositoryAction(async () => await _notificationService.UpdateAsync(notificationDtoUpdate));
        }

        /*
        [HttpDelete("DeleteNotification")]
        public async Task<IActionResult> Delete(Notification notification)
        {

            return await HandleRepositoryAction(async () => await _notificationRepository.Remove(notification));
        }*/
    }
}
