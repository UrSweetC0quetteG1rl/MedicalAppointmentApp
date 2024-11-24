using MedicalAppointment.Persistance.Interfaces.Configuration.Medical;
using MedicalAppointmentApp.Domain.Entities.Medical;
using Microsoft.AspNetCore.Mvc;



namespace MedicalAppointmentApp.Medical.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordsController : ControllerBase
    {

        private readonly IMedicalRecordsRepository _medicalRecordsRepository;

        public MedicalRecordsController(IMedicalRecordsRepository medicalRecordsRepository)
        {
            _medicalRecordsRepository = medicalRecordsRepository;
        }



        [HttpGet("GetSpecialties")]
        public async Task<IActionResult> GetSpecialties()
        {
            var result = await _medicalRecordsRepository.GetAll();
            if (result == null)
                return NotFound("No se encontraron datos");

            return Ok(result);
        }




        [HttpPost("SavesAvailibitymode")]
        public async Task<IActionResult> Post([FromBody] MedicalRecords medicalRecordsRepository)
        {
            var result = await _medicalRecordsRepository.Save(medicalRecordsRepository);

            if (!result.Success)
            {
                return BadRequest(result);
            }


            return Ok();

        }


        [HttpPost("ModifyAvailibitymode")]
        public async Task<IActionResult> Put([FromBody] MedicalRecords medicalRecordsRepository)
        {
            var result = await _medicalRecordsRepository.Update(medicalRecordsRepository);

            if (!result.Success)
            {
                return BadRequest(result);
            }


            return Ok();
        }


        [HttpPost("DisableAvailibitymode")]
        public async Task<IActionResult> DisableNetwork(MedicalRecords medicalRecordsRepository)
        {
            var result = await _medicalRecordsRepository.Remove(medicalRecordsRepository);

            if (!result.Success)
            {
                return BadRequest(result);
            }


            return Ok();
        }
    }
}
