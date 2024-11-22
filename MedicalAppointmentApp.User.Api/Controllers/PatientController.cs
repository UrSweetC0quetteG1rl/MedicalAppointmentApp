using MedicalAppointment.Persistance.Interfaces.Users;
using MedicalAppointmentApp.Application.Contracts.Users;
using MedicalAppointmentApp.Application.Dtos.Users.Patient;
using MedicalAppointmentApp.Domain.Entities.User;
using Microsoft.AspNetCore.Mvc;


namespace MedicalAppointmentApp.Users.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly    IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }


        
        [HttpGet("GetPatients")]
        public async Task<IActionResult> Get()
        {
            var result = await _patientService.GetAll();
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        
        [HttpGet("GetPatientById")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _patientService.GetById(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        
        [HttpPost("SavePatient")]
        public async Task<IActionResult> Post([FromBody] PatientDtoSave patientDtoSave)
        {
            var result = await _patientService.SaveAsync(patientDtoSave);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok();
        }

        
        [HttpPut("UpdatePatient")]
        public async Task<IActionResult> Put([FromBody] PatientDtoUpdate patientDtoUpdate)
        {
            var result = await _patientService.UpdateAsync(patientDtoUpdate);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok();
        }

        
        [HttpDelete("DeletePatient")]
        public async Task<IActionResult> Delete(PatientDtoUpdate patientDtoUpdate)
        {
            var result = await _patientService.UpdateAsync(patientDtoUpdate);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }


            return Ok();
        }
    }
}
