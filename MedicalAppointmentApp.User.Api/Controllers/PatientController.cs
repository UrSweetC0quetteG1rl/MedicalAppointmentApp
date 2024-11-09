using MedicalAppointment.Persistance.Interfaces.Users;
using MedicalAppointment.Persistance.Repositories.Users;
using MedicalAppointmentApp.Domain.Entities.User;
using MedicalAppointmentApp.System.Api.Base;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace MedicalAppointmentApp.Users.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;

        public PatientController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }


        
        [HttpGet("GetPatients")]
        public async Task<IActionResult> Get()
        {
            var result = await _patientRepository.GetAll();
            if (result == null)
                return NotFound("No se encontraron datos");

            return Ok(result);
        }

        
        [HttpGet("GetPatientById")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _patientRepository.GetEntityBy(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        
        [HttpPost("SavePatient")]
        public async Task<IActionResult> Post([FromBody] Patient patient)
        {
            var result = await _patientRepository.Save(patient);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok();
        }

        
        [HttpPut("UpdatePatient")]
        public async Task<IActionResult> Put([FromBody] Patient patient)
        {
            var result = await _patientRepository.Update(patient);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok();
        }

        
        [HttpDelete("DeletePatient")]
        public async Task<IActionResult> Delete(Patient patient)
        {
            var result = await _patientRepository.Remove(patient);

            if (!result.Success)
            {
                return BadRequest(result);
            }


            return Ok();
        }
    }
}
