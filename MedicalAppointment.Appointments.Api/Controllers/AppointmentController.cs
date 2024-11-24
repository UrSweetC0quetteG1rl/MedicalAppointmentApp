using MedicalAppointment.Persistance.Interfaces.Configuration.Appointments;
using MedicalAppointment.Persistance.Interfaces.Configuration.Insurance;
using MedicalAppointment.Persistance.Interfaces.Configuration.Medical;
using MedicalAppointment.Persistance.Repositories.Configuration.Appointments;
using MedicalAppointmentApp.Domain.Entities.Appoinments;
using MedicalAppointmentApp.Domain.Entities.Insurance;
using MedicalAppointmentApp.Domain.Entities.Medical;
using Microsoft.AspNetCore.Mvc;



namespace MedicalAppointmentApp.Medical.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {

        private readonly IMedicalAppointmentRepository _medicalAppointmentRepository;

        public AppointmentController(IMedicalAppointmentRepository appointmentRepository)
        {
            _medicalAppointmentRepository = appointmentRepository;
        }



        [HttpGet("GetAppointment")]
        public async Task<IActionResult> GetAppointment()
        {
            var result = await _medicalAppointmentRepository.GetAll();
            if (result == null)
                return NotFound("No se encontraron datos");

            return Ok(result);
        }


     

        [HttpPost("SavesAppointment")]
        public async Task<IActionResult> Post([FromBody] Appointment appointment)
        {
            var result = await _medicalAppointmentRepository.Save(appointment);

            if (!result.Success)
            {
                return BadRequest(result);
            }


            return Ok();

        }


        [HttpPost("ModifyAppointment")]
        public async Task<IActionResult> Put([FromBody] Appointment appointment)
        {
            var result = await _medicalAppointmentRepository.Update(appointment);

            if (!result.Success)
            {
                return BadRequest(result);
            }


            return Ok();
        }


        [HttpPost("DisableAppointment")]
        public async Task<IActionResult> DisableAppointment(Appointment appointment)
        {
            var result = await _medicalAppointmentRepository.Remove(appointment);

            if (!result.Success)
            {
                return BadRequest(result);
            }


            return Ok();
        }
    }
}
