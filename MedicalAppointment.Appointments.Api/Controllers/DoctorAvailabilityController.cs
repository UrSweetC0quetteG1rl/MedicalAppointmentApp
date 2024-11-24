using MedicalAppointment.Persistance.Interfaces.Configuration.Appointments;
using MedicalAppointmentApp.Domain.Entities.Appoinments;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicalAppointment.Appointments.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorAvailabilityController : ControllerBase
    {

        private readonly IDoctorAvailabilityRepository? _doctorAvailabilityrepository;

        // GET: api/<DoctorAvailabilityController>
        [HttpGet("GetDoctorAvailability")]
        public async Task<IActionResult> GetDoctorAvailability()
        {
            var result = await _doctorAvailabilityrepository.GetAll();
            if (result == null)
                return NotFound("No se encontraron datos");

            return Ok(result);
        }

        [HttpPost("SavesDoctorAvailability")]
        public async Task<IActionResult> Post([FromBody] DoctorAvailability doctorAvailability)
        {
            var result = await _doctorAvailabilityrepository.Save(doctorAvailability);

            if (!result.Success)
            {
                return BadRequest(result);
            }


            return Ok();

        }

        [HttpPost("ModifyDoctorAvailability")]
        public async Task<IActionResult> Put([FromBody] DoctorAvailability doctorAvailability)
        {
            var result = await _doctorAvailabilityrepository.Update(doctorAvailability);

            if (!result.Success)
            {
                return BadRequest(result);
            }


            return Ok();
        }

        [HttpPost("DisableDoctorAvailability")]
        public async Task<IActionResult> DisableAppointment(DoctorAvailability doctorAvailability)
        {
            var result = await _doctorAvailabilityrepository.Remove(doctorAvailability);

            if (!result.Success)
            {
                return BadRequest(result);
            }


            return Ok();
        }
    }
}
