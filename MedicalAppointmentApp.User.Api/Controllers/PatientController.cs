using MedicalAppointment.Persistance.Interfaces.Users;
using MedicalAppointmentApp.Domain.Entities.User;
using MedicalAppointmentApp.System.Api.Base;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointmentApp.Users.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : BaseController
    {
        private readonly IPatientRepository _patientRepository;

        public PatientController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }


        
        [HttpGet("GetPatients")]
        public async Task<IActionResult> Get()
        {
            return await HandleRepositoryAction(async () => await _patientRepository.GetAll());
        }

        
        [HttpGet("GetPatientById")]
        public async Task<IActionResult> Get(int id)
        {
            return await HandleRepositoryAction(async () => await _patientRepository.GetEntityBy(id));
        }

        
        [HttpPost("SavePatient")]
        public async Task<IActionResult> Post([FromBody] Patient patient)
        {
            return await HandleRepositoryAction(async() => await _patientRepository.Save(patient));
        }

        
        [HttpPut("UpdatePatient")]
        public async Task<IActionResult> Put([FromBody] Patient patient)
        {
            return await HandleRepositoryAction(async() => await _patientRepository.Update(patient));
        }

        
        [HttpDelete("DeletePatient")]
        public async Task<IActionResult> Delete(Patient patient)
        {
            return await HandleRepositoryAction(async() => await _patientRepository.Remove(patient));
        }
    }
}
