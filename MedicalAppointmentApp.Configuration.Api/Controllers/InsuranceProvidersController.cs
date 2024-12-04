using MedicalAppointment.Aplication.Contracts.Insurance;
using MedicalAppointment.Aplication.Dtos.Configuration.Insurnace.InsuranceProviders;
using Microsoft.AspNetCore.Mvc;



namespace MedicalAppointmentApp.Configuration.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceProvidersController : ControllerBase
    {

        private readonly IInsuranceProviders _insuranceProviders;


        public InsuranceProvidersController (IInsuranceProviders insuranceProvidersRepository)
        {
            _insuranceProviders = insuranceProvidersRepository;
        }


        [HttpGet("GetInsutanceProviders")]
        public async Task<IActionResult> GetInsuranceProviders()
        {
            var result = await _insuranceProviders.GetAll();
            if (!result.IsSuccess)
                return NotFound("No se encontraron datos");

            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetInsuranceProviders(int id)
        {
            var result = await _insuranceProviders.GetById(id);
           
            if (result == null)
                return NotFound("No se encontraron datos");

            return Ok(result);
        }


        [HttpPost("SavesInsurance")]
        public async Task<IActionResult> Post([FromBody] InsuranceProvidersSaveDto insuranceProvidersSaveDto)
        {
            var result = await _insuranceProviders.SaveAsync(insuranceProvidersSaveDto);
           

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }


            return Ok();

        }


        [HttpPost("UpdateInsurance")]
        public async Task<IActionResult> Put([FromBody] InsuranceProvidersUpdateDto insuranceProvidersUpdateDto)
        {
            var result = await _insuranceProviders.UpdateAsync(insuranceProvidersUpdateDto);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }


            return Ok();
        }


       
    }
}
