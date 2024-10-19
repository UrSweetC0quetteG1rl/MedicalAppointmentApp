using MedicalAppointment.Persistance.Interfaces.Configuration.Insurance;
using MedicalAppointment.Persistance.Repositories.Configuration.Insurance;
using MedicalAppointmentApp.Domain.Entities.Insurance;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicalAppointmentApp.Configuration.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceProvidersController : ControllerBase
    {

        private readonly IInsuranceProvidersRepository _insuranceProvidersRepository;


        public InsuranceProvidersController (IInsuranceProvidersRepository insuranceProvidersRepository)
        {
            _insuranceProvidersRepository = insuranceProvidersRepository;
        }


        [HttpGet("GetInsutanceProviders")]
        public async Task<IActionResult> GetNetworkTypes()
        {
            var result = await _insuranceProvidersRepository.GetAll();
            if (result == null)
                return NotFound("No se encontraron datos");

            return Ok(result);
        }


        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }


        [HttpPost("SavesInsurance")]
        public async Task<IActionResult> Post([FromBody] InsuranceProviders InsuranceProviders)
        {
            var result = await _insuranceProvidersRepository.Save(InsuranceProviders);

            if (!result.Success)
            {
                return BadRequest(result);
            }


            return Ok();

        }


        [HttpPost("ModifyInsurance")]
        public async Task<IActionResult> Put([FromBody] InsuranceProviders InsuranceProviders)
        {
            var result = await _insuranceProvidersRepository.Update(InsuranceProviders);

            if (!result.Success)
            {
                return BadRequest(result);
            }


            return Ok();
        }


        [HttpPost("DisableInsurance")]
        public async Task<IActionResult> DisableNetwork(InsuranceProviders InsuranceProviders)
        {
            var result = await _insuranceProvidersRepository.Remove(InsuranceProviders);

            if (!result.Success)
            {
                return BadRequest(result);
            }


            return Ok();
        }
    }
}
