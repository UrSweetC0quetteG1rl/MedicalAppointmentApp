using MedicalAppointment.Persistance.Interfaces.Configuration.Insurance;
using MedicalAppointment.Persistance.Interfaces.Configuration.Medical;
using MedicalAppointmentApp.Domain.Entities.Insurance;
using MedicalAppointmentApp.Domain.Entities.Medical;
using Microsoft.AspNetCore.Mvc;



namespace MedicalAppointmentApp.Medical.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailabilityModesController : ControllerBase
    {

        private readonly IAvailabilityModesRepository _availibityModesRepository;

        public AvailabilityModesController(IAvailabilityModesRepository availabilityRepository)
        {
            _availibityModesRepository = availabilityRepository;
        }



        [HttpGet("GetAvailibitymode")]
        public async Task<IActionResult> GetAvailibitymode()
        {
            var result = await _availibityModesRepository.GetAll();
            if (result == null)
                return NotFound("No se encontraron datos");

            return Ok(result);
        }


     

        [HttpPost("SavesAvailibitymode")]
        public async Task<IActionResult> Post([FromBody] AvailabilityModes availabilityModes)
        {
            var result = await _availibityModesRepository.Save(availabilityModes);

            if (!result.Success)
            {
                return BadRequest(result);
            }


            return Ok();

        }


        [HttpPost("ModifyAvailibitymode")]
        public async Task<IActionResult> Put([FromBody] AvailabilityModes availabilityModes)
        {
            var result = await _availibityModesRepository.Update(availabilityModes);

            if (!result.Success)
            {
                return BadRequest(result);
            }


            return Ok();
        }


        [HttpPost("DisableAvailibitymode")]
        public async Task<IActionResult> DisableNetwork(AvailabilityModes availabilityModes)
        {
            var result = await _availibityModesRepository.Remove(availabilityModes);

            if (!result.Success)
            {
                return BadRequest(result);
            }


            return Ok();
        }
    }
}
