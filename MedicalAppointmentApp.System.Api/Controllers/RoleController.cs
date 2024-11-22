
using MedicalAppointmentApp.Application.Contracts.System;
using MedicalAppointmentApp.Application.Dtos.System.Role;
using MedicalAppointmentApp.System.Api.Base;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointmentApp.System.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BaseController
    {

        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // GET: api/<RoleController>
        [HttpGet("GetRoles")]
        public async Task<IActionResult> Get()
        {
            return await HandleRepositoryAction(async () => await _roleService.GetAll());
        }

        [HttpGet("GetRoleById")]
        public async Task<IActionResult> Get(int id)
        {
            return await HandleRepositoryAction(async () => await _roleService.GetById(id));
        }

        [HttpPost("SaveRole")]
        public async Task<IActionResult> Post([FromBody] RoleDtoSave roleDtoSave)
        {
            return await HandleRepositoryAction(async () => await _roleService.SaveAsync(roleDtoSave));
        }

        [HttpPut("UpdateRole")]
        public async Task<IActionResult> Put([FromBody] RoleDtoUpdate roleDtoUpdate)
        {
            var result = await _roleService.UpdateAsync(roleDtoUpdate);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok();
            /*
            return await HandleRepositoryAction(async () => await _roleService.UpdateAsync(roleDtoUpdate));*/
        }
        /*
        [HttpDelete("DeleteRole")]
        public async Task<IActionResult> Delete(Role role)
        {
            return await HandleRepositoryAction(async () => await _roleRepository.Remove(role));
        }*/
    }
}
