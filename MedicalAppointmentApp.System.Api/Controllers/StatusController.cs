
using MedicalAppointmentApp.Application.Contracts.System;
using MedicalAppointmentApp.Application.Dtos.System.Status;
using MedicalAppointmentApp.System.Api.Base;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointmentApp.System.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : BaseController
    {
        private readonly IStatusService _statusService;

        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        
        [HttpGet("GetStatus")]
        public async Task<IActionResult> Get()
        {
            return await HandleRepositoryAction(async () => await _statusService.GetAll());
        }

        
        [HttpGet("GetStatusById")]
        public async Task<IActionResult> Get(int id)
        {
            return await HandleRepositoryAction(async () => await _statusService.GetById(id));
        }

        
        [HttpPost("SaveStatus")]
        public async Task<IActionResult> Post([FromBody] StatusDtoSave statusDtoSave)
        {
            return await HandleRepositoryAction(async () => await _statusService.SaveAsync(statusDtoSave));
        }

       
        [HttpPut("UpdateStatus")]
        public async Task<IActionResult> Put([FromBody] StatusDtoUpdate statusDtoUpdate)
        {
            return await HandleRepositoryAction(async () => await _statusService.UpdateAsync(statusDtoUpdate));
        }

        /*
        [HttpDelete("DeleteStatus")]
        public async Task<IActionResult> Delete(Status status)
        {
            return await HandleRepositoryAction(async () => await _statusRepository.Remove(status));
        }*/
    }
}
