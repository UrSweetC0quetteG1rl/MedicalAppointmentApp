using MedicalAppointment.Aplication.Contracts.Insurance;
using MedicalAppointment.Aplication.Dtos.Configuration.Insurnace.NetworkType;
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
        private readonly INetworkType _networkTypeRepository;

        public NetworkTypeController(INetworkType networkTypeRepository) 
        {
           _networkTypeRepository = networkTypeRepository;
        }


        
        [HttpGet("GetNetWorkType")]
        public async Task<IActionResult> GetNetworkTypes()
        {
            var result = await _networkTypeRepository.GetAll();
            if (!result.IsSuccess)
                return NotFound("No se encontraron datos");

            return Ok(result);
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNetworkTypesById(int id)
        {
            var result = await _networkTypeRepository.GetById(id);

            if (!result.IsSuccess)
                return NotFound("No se encontraron datos");

            return Ok(result);
        }

        [HttpPost("SavesNet")]
        public async Task<IActionResult> Post([FromBody] NetworkSaveDto networkSaveDto)
        {
            var result = await _networkTypeRepository.SaveAsync(networkSaveDto);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }


            return Ok();

        }

       
        [HttpPost("UpdateNetwork")]
        public async Task<IActionResult> Put([FromBody] NetworkUpdateDto networkUpdateDto)
        {
            var result = await _networkTypeRepository.UpdateAsync(networkUpdateDto);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }


            return Ok();
        }


     

    }
}
