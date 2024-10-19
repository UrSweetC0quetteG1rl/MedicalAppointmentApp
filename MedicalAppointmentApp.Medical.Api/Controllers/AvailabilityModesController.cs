using MedicalAppointment.Persistance.Interfaces.Configuration.Insurance;
using MedicalAppointment.Persistance.Interfaces.Configuration.Medical;
using MedicalAppointmentApp.Domain.Entities.Insurance;
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



        [HttpGet("GetNetWorkType")]
        public async Task<IActionResult> GetNetworkTypes()
        {
            var result = await _networkTypeRepository.GetAll();
            if (result == null)
                return NotFound("No se encontraron datos");

            return Ok(result);
        }


        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }


        [HttpPost("SavesNet")]
        public async Task<IActionResult> Post([FromBody] NetworkType networkType)
        {
            var result = await _networkTypeRepository.Save(networkType);

            if (!result.Success)
            {
                return BadRequest(result);
            }


            return Ok();

        }


        [HttpPost("ModifyNetwork")]
        public async Task<IActionResult> Put([FromBody] NetworkType networkType)
        {
            var result = await _networkTypeRepository.Update(networkType);

            if (!result.Success)
            {
                return BadRequest(result);
            }


            return Ok();
        }


        [HttpPost("DisableNetwork")]
        public async Task<IActionResult> DisableNetwork(NetworkType networkType)
        {
            var result = await _networkTypeRepository.Remove(networkType);

            if (!result.Success)
            {
                return BadRequest(result);
            }


            return Ok();
        }
    }
}
