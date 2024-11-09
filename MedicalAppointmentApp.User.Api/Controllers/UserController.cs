using MedicalAppointmentApp.Domain.Entities.User;
using MedicalAppointment.Persistance.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;


namespace MedicalAppointmentApp.Users.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        
        [HttpGet("GetUsers")]
        public async Task<IActionResult> Get()
        {
            
            var result = await _userRepository.GetAll();
            if (result == null)
                return NotFound("No se encontraron datos");

            return Ok(result);
        }

        
        [HttpGet("GetUserById")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _userRepository.GetEntityBy(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        
        [HttpPost("SaveUser")]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            var result = await _userRepository.Save(user);

            if (!result.Success)
            {
                return BadRequest(result);
            }


            return Ok();
        }

        
        [HttpPut("UpdateUser")]
        public async Task<IActionResult>  Put([FromBody] User user)
        {
            var result = await _userRepository.Update(user);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok();
        }

        
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> Delete(User user)
        {
            var result = await _userRepository.Remove(user);

            if (!result.Success)
            {
                return BadRequest(result);
            }


            return Ok();

        }
    }
}
