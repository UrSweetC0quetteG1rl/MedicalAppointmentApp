using MedicalAppointment.Persistance.Interfaces.System;
using MedicalAppointmentApp.Domain.Entities.System;
using MedicalAppointmentApp.System.Api.Base;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointmentApp.System.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BaseController
    {

        private readonly IRoleRepository _roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        // GET: api/<RoleController>
        [HttpGet("GetRoles")]
        public async Task<IActionResult> Get()
        {
            return await HandleRepositoryAction(async () => await _roleRepository.GetAll());
        }

        [HttpGet("GetRoleById")]
        public async Task<IActionResult> Get(int id)
        {
            return await HandleRepositoryAction(async () => await _roleRepository.GetEntityBy(id));
        }

        [HttpPost("SaveRole")]
        public async Task<IActionResult> Post([FromBody] Role role)
        {
            return await HandleRepositoryAction(async () => await _roleRepository.Save(role));
        }

        [HttpPut("UpdateRole")]
        public async Task<IActionResult> Put([FromBody] Role role)
        {
            return await HandleRepositoryAction(async () => await _roleRepository.Update(role));
        }

        [HttpDelete("DeleteRole")]
        public async Task<IActionResult> Delete(Role role)
        {
            return await HandleRepositoryAction(async () => await _roleRepository.Remove(role));
        }
    }
}
