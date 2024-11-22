using MedicalAppointment.Persistance.Interfaces.Users;
using MedicalAppointmentApp.Application.Contracts.Users;
using MedicalAppointmentApp.Application.Dtos.Users.Doctor;
using MedicalAppointmentApp.Domain.Entities.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace MedicalAppointmentApp.Users.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }


        
        [HttpGet("GetDoctors")]
        public async Task<IActionResult> Get()
        {
            var result = await _doctorService.GetAll();
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        
        [HttpGet("GetDoctorById")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _doctorService.GetById(id);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /*
        [HttpGet("GetDoctorBySpecialty")]
        public async Task<IActionResult> Get(short id)
        {
            var result = await _doctorRepository.GetDoctorsBySpecialty(id);

            if (!result.Success) { return BadRequest(result); }

            return Ok(result);
        }*/

        [HttpPost("SaveDoctor")]
        public async Task<IActionResult> Post([FromBody] DoctorDtoSave doctorDtoSave)
        {
            var result = await _doctorService.SaveAsync(doctorDtoSave);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok();
        }

        
        [HttpPut("UpdateDoctor")]
        public async Task<IActionResult> Put([FromBody] DoctorDtoUpdate doctorDtoUpdate)
        {
            var result = await _doctorService.UpdateAsync(doctorDtoUpdate);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok();
        }

        /*
        [HttpPut("DeactivateDoctor")]
        public async Task<IActionResult> Put(int Id)
        {
            var result = await _doctorRepository.DeactivateDoctorById(Id);

            if (!result.Success) { return BadRequest(result); }

            return Ok(result);
        }


        [HttpDelete("DeleteDoctor")]
        public async Task<IActionResult> Delete(Doctor doctor)
        {
            var result = await _doctorRepository.Remove(doctor);

            if (!result.Success)
            {
                return BadRequest(result);
            }


            return Ok();
        }*/
    }
}
