using MedicalAppointment.Persistance.Models.System;

using MedicalAppointmentApp.Application.Contracts.System;
using MedicalAppointmentApp.Application.Dtos.System.Role;
using MedicalAppointmentApp.Domain.Entities.System;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointmentApp.Web.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _roleService.GetAll();

            return result.IsSuccess ? View(result.Data as List<Role>) : View();
        }

        
        public async Task<IActionResult> Details(int id)
        {
            var result = await _roleService.GetById(id);
            return result.IsSuccess ? View(result.Data as Role) : View();
        }

        
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleDtoSave roleDtoSave)
        {
            roleDtoSave.CreatedAt = DateTime.Now;

            var result = await _roleService.SaveAsync(roleDtoSave);
            if (result.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Message = result.Message;
            return View();
        }

        
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _roleService.GetById(id);
            return result.IsSuccess ? View(result.Data as Role) : View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoleDtoUpdate roleDtoUpdate)
        {
            roleDtoUpdate.UpdatedAt = DateTime.Now;

            var result = await _roleService.UpdateAsync(roleDtoUpdate);
            if (result.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Message = result.Message;
            return View();
        }

        /*
        public ActionResult Delete(int id)
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/
    }
}
