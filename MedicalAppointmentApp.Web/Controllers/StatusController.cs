using MedicalAppointment.Persistance.Models.User;
using MedicalAppointmentApp.Application.Contracts.System;
using MedicalAppointmentApp.Application.Contracts.Users;
using MedicalAppointmentApp.Application.Dtos.System.Status;
using MedicalAppointmentApp.Application.Dtos.Users.User;
using MedicalAppointmentApp.Application.Services.Users;
using MedicalAppointmentApp.Domain.Entities.System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointmentApp.Web.Controllers
{
    public class StatusController : Controller
    {
        private readonly IStatusService _statusService;

        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _statusService.GetAll();

            return result.IsSuccess ? View(result.Data as List<Status>) : View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await _statusService.GetById(id);
            return result.IsSuccess ? View(result.Data as Status) : View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StatusDtoSave statusDtoSave)
        {
            

            var result = await _statusService.SaveAsync(statusDtoSave);
            if (result.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Message = result.Message;
            return View();
        }

        
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _statusService.GetById(id);
            return result.IsSuccess ? View(result.Data as Status) : View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StatusDtoUpdate statusDtoUpdate)
        {
            

            var result = await _statusService.UpdateAsync(statusDtoUpdate);
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

        // POST: StatusController/Delete/5
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
