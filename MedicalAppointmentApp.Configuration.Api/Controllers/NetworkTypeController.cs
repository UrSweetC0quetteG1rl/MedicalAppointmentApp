using MedicalAppointment.Persistance.Interfaces.Configuration.Insurance;
using MedicalAppointment.Persistance.Repositories.Configuration.Insurance;
using MedicalAppointmentApp.Domain.Entities.Insurance;
using Microsoft.AspNetCore.Mvc;



namespace MedicalAppointmentApp.Configuration.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NetworkTypeController : ControllerBase
    {
        private readonly INetworkTypeRepository _networkTypeRepository;

        public NetworkTypeController(INetworkTypeRepository networkTypeRepository) 
        {
           _networkTypeRepository = networkTypeRepository;
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
        public async Task<IActionResult> GetNetworkTypes(int id)
        {
            var result = await _networkTypeRepository.GetEntityBy(id);

            if (result == null)
                return NotFound("No se encontraron datos");

            return Ok(result);
        }

        [HttpPost("SavesNet")]
        public async Task<IActionResult> Post([FromBody] NetworkType networkType)
        {
            var result = await _networkTypeRepository?.Save(networkType);

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
