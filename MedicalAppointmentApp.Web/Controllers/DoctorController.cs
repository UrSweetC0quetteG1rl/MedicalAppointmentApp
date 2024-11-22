using MedicalAppointment.Persistance.Models.User;
using MedicalAppointmentApp.Application.Contracts.Users;
using MedicalAppointmentApp.Application.Dtos.Users.Doctor;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointmentApp.Web.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _doctorService.GetAll();

            return result.IsSuccess ? View(result.Data as List<DoctorSpecialtyAvailabilityModel>) : View();
        }

        
        public async Task<IActionResult> Details(int id)
        {
            var result = await _doctorService.GetById(id);
            return result.IsSuccess ? View(result.Data as DoctorSpecialtyAvailabilityModel) : View();
        }


        public ActionResult Create() => View();

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DoctorDtoSave doctorDtoSave)
        {
            doctorDtoSave.CreatedAt = DateTime.Now;

            var result = await _doctorService.SaveAsync(doctorDtoSave);
            if (result.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Message = result.Message;
            return View();
        }

        
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _doctorService.GetById(id);

            if (result.IsSuccess)
            {
                DoctorSpecialtyAvailabilityModel doctorSpecialtyAvailabilityModel = (DoctorSpecialtyAvailabilityModel)result.Data;
                return View(doctorSpecialtyAvailabilityModel);
            }
            return View();
            /*
            var result = await _doctorService.GetById(id);
            return result.IsSuccess ? View(result.Data as DoctorSpecialtyAvailabilityModel) : View();*/
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DoctorDtoUpdate doctorDtoUpdate)
        {
            try
            {
                doctorDtoUpdate.UpdatedAt = DateTime.Now;
                var result = await _doctorService.UpdateAsync(doctorDtoUpdate);

                if (result.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = result.Message;
                    return View();
                }

            }
            catch
            {
                return View();
            }
            /*
            doctorDtoUpdate.UpdatedAt = DateTime.Now;

            var result = await _doctorService.UpdateAsync(doctorDtoUpdate);
            if (result.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Message = result.Message;
            return View();*/
        }
        /*
        // GET: DoctorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DoctorController/Delete/5
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
