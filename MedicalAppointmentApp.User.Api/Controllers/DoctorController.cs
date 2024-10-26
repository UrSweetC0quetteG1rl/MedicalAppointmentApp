using MedicalAppointment.Persistance.Interfaces.Users;
using MedicalAppointmentApp.Domain.Entities.User;
using MedicalAppointmentApp.System.Api.Base;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointmentApp.Users.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : BaseController
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorController(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }


        
        [HttpGet("GetDoctors")]
        public async Task<IActionResult> Get()
        {
            return await HandleRepositoryAction(async () => await _doctorRepository.GetAll());
        }

        
        [HttpGet("GetDoctorById")]
        public async Task<IActionResult> Get(int id)
        {
            return await HandleRepositoryAction(async () => await _doctorRepository.GetEntityBy(id));
        }

        
        [HttpPost("SaveDoctor")]
        public async Task<IActionResult> Post([FromBody] Doctor doctor)
        {
            return await HandleRepositoryAction(async() => await _doctorRepository.Save(doctor));
        }

        
        [HttpPut("UpdateDoctor")]
        public async Task<IActionResult> Put([FromBody] Doctor doctor)
        {
            return await HandleRepositoryAction(async() => await _doctorRepository.Update(doctor));
        }

        
        [HttpDelete("DeleteDoctor")]
        public async Task<IActionResult> Delete(Doctor doctor)
        {
            return await HandleRepositoryAction(async() => await _doctorRepository.Remove(doctor));
        }
    }
}
