using MedicalAppointment.Aplication.Contracts.Appointment;
using MedicalAppointment.Aplication.Dtos.Configuration.Appointment.DoctorAvailability;
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

        private readonly IDoctorAvailability _doctorAvailabilityrepository;


        public DoctorAvailabilityController(IDoctorAvailability doctorAvailabilityrepository)
        {
            _doctorAvailabilityrepository = doctorAvailabilityrepository;
        }
        
        [HttpGet("GetDoctorAvailability")]
        public async Task<IActionResult> GetDoctorAvailability()
        {
            var result = await _doctorAvailabilityrepository.GetAll();
            if (result == null)
                return NotFound("No se encontraron datos");

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointment(int id)
        {
            var result = await _doctorAvailabilityrepository.GetById(id);

            if (result == null)
                return NotFound("No se encontraron datos");

            return Ok(result);
        }

        [HttpPost("SavesDoctorAvailability")]
        public async Task<IActionResult> Post([FromBody] DoctorAvailabilitySaveDto doctorAvailabilitySaveDto)
        {
            var result = await _doctorAvailabilityrepository.SaveAsync(doctorAvailabilitySaveDto);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }


            return Ok();

        }

        [HttpPost("UpdateDoctorAvailability")]
        public async Task<IActionResult> Put([FromBody] DoctorAvailabilityUpdateDto doctorAvailabilityUpdateDto)
        {
            var result = await _doctorAvailabilityrepository.UpdateAsync(doctorAvailabilityUpdateDto);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }


            return Ok();
        }

       
    }
}
