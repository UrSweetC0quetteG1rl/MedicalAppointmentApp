using MedicalAppointment.Persistance.Interfaces.Configuration.Medical;
using MedicalAppointment.Persistance.Repositories.Configuration.Medical;
using MedicalAppointmentApp.Domain.Entities.Medical;
using Microsoft.AspNetCore.Mvc;



namespace MedicalAppointmentApp.Medical.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialtiesController : ControllerBase
    {

        private readonly ISpecialtiesRepository _specialtiesRepository;

        public SpecialtiesController(ISpecialtiesRepository specialtiesRepository)
        {
            _specialtiesRepository = specialtiesRepository;
        }



        [HttpGet("GetSpecialties")]
        public async Task<IActionResult> GetAvailibitymode()
        {
            var result = await _specialtiesRepository.GetAll();
            if (result == null)
                return NotFound("No se encontraron datos");

            return Ok(result);
        }




        [HttpPost("SavesSpecialties")]
        public async Task<IActionResult> Post([FromBody] Specialties specialtiesRepository)
        {
            var result = await _specialtiesRepository.Save(specialtiesRepository);

            if (!result.Success)
            {
                return BadRequest(result);
            }


            return Ok();

        }


        [HttpPost("ModifySpecialties")]
        public async Task<IActionResult> Put([FromBody] Specialties specialtiesRepository)
        {
            var result = await _specialtiesRepository.Update(specialtiesRepository);

            if (!result.Success)
            {
                return BadRequest(result);
            }


            return Ok();
        }


        [HttpPost("DisableSpecialties")]
        public async Task<IActionResult> DisableSpecialties(Specialties specialtiesRepository)
        {
            var result = await _specialtiesRepository.Remove(specialtiesRepository);

            if (!result.Success)
            {
                return BadRequest(result);
            }


            return Ok();
        }
    }
}
