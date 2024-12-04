using MedicalAppointment.Aplication.Contracts.Appointment;
using MedicalAppointment.Aplication.Dtos.Configuration.Appointment.Appointments;
using MedicalAppointment.Persistance.Interfaces.Configuration.Appointments;
using MedicalAppointmentApp.Domain.Entities.Appoinments;
using Microsoft.AspNetCore.Mvc;



namespace MedicalAppointmentApp.Medical.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {

        private readonly IAppointment _medicalAppointmentRepository;

        public AppointmentController(IAppointment appointmentRepository)
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
        public async Task<IActionResult> Post([FromBody] AppointmentSaveDto appointmentSaveDto)
        {
            var result = await _medicalAppointmentRepository.SaveAsync(appointmentSaveDto);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }


            return Ok();

        }


        [HttpPost("UpdateAppointment")]
        public async Task<IActionResult> Put([FromBody] AppointmentUpdateDto appointmentUpdateDto)
        {
            var result = await _medicalAppointmentRepository.UpdateAsync(appointmentUpdateDto);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }


            return Ok();
        }


       
    }
}
