using MedicalAppointmentApp.Domain.Entities.User;
using MedicalAppointment.Persistance.Interfaces.Users;
using MedicalAppointmentApp.System.Api.Base;
using Microsoft.AspNetCore.Mvc;


namespace MedicalAppointmentApp.Users.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        
        [HttpGet("GetUsers")]
        public async Task<IActionResult> Get()
        {
            return await HandleRepositoryAction(_userRepository.GetAll);
        }

        
        [HttpGet("GetUserById")]
        public async Task<IActionResult> Get(int id)
        {
            return await HandleRepositoryAction(async () => await _userRepository.GetEntityBy(id));
        }

        
        [HttpPost("SaveUser")]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            return await HandleRepositoryAction(async () => await _userRepository.Save(user));
        }

        
        [HttpPut("UpdateUser")]
        public async Task<IActionResult>  Put([FromBody] User user)
        {
            return await HandleRepositoryAction(async () => await _userRepository.Update(user));
        }

        
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> Delete(User user)
        {
            return await HandleRepositoryAction(async () => await _userRepository.Remove(user));
        }
    }
}
