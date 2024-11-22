
using Microsoft.AspNetCore.Mvc;
using MedicalAppointmentApp.Application.Contracts.Users;
using MedicalAppointmentApp.Application.Dtos.Users.User;


namespace MedicalAppointmentApp.Users.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        
        [HttpGet("GetUsers")]
        public async Task<IActionResult> Get()
        {
            
            var result = await _userService.GetAll();
            if (!result.IsSuccess) {return BadRequest(result); }
                

            return Ok(result);
        }

        
        [HttpGet("GetUserById")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _userService.GetById(id);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        
        [HttpPost("SaveUser")]
        public async Task<IActionResult> Post([FromBody] UserDtoSave userDtoSave)
        {
            var result = await _userService.SaveAsync(userDtoSave);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }


            return Ok();
        }

        
        [HttpPut("UpdateUser")]
        public async Task<IActionResult>  Put([FromBody] UserDtoUpdate userDtoUpdate)
        {
            var result = await _userService.UpdateAsync(userDtoUpdate);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok();
        }

        
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> Delete(UserDtoUpdate userDtoUpdate)
        {
            var result = await _userService.UpdateAsync(userDtoUpdate);//recuerda modificar el remove agregalo a los servicios


            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }


            return Ok();

        }
    }
}
