using MedicalAppointment.Persistance.Interfaces.System;
using MedicalAppointmentApp.Domain.Entities.System;
using MedicalAppointmentApp.System.Api.Base;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointmentApp.System.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : BaseController
    {
        private readonly IStatusRepository _statusRepository;

        public StatusController(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        
        [HttpGet("GetStatus")]
        public async Task<IActionResult> Get()
        {
            return await HandleRepositoryAction(async () => await _statusRepository.GetAll());
        }

        
        [HttpGet("GetStatusById")]
        public async Task<IActionResult> Get(int id)
        {
            return await HandleRepositoryAction(async () => await _statusRepository.GetEntityBy(id));
        }

        
        [HttpPost("SaveStatus")]
        public async Task<IActionResult> Post([FromBody] Status status)
        {
            return await HandleRepositoryAction(async () => await _statusRepository.Save(status));
        }

       
        [HttpPut("UpdateStatus")]
        public async Task<IActionResult> Put([FromBody] Status status)
        {
            return await HandleRepositoryAction(async () => await _statusRepository.Update(status));
        }

        
        [HttpDelete("DeleteStatus")]
        public async Task<IActionResult> Delete(Status status)
        {
            return await HandleRepositoryAction(async () => await _statusRepository.Remove(status));
        }
    }
}
