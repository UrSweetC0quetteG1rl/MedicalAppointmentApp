
using MedicalAppointment.Persistance.Models.User;
using MedicalAppointmentApp.Application.Contracts.Users;
using MedicalAppointmentApp.Application.Dtos.Users.Patient;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointmentApp.Web.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _patientService.GetAll();

            return result.IsSuccess ? View(result.Data as List<PatientInsurenceProviderModel>) : View();
        }

        
        public async Task<IActionResult> Details(int id)
        {
            var result = await _patientService.GetById(id);
            return result.IsSuccess ? View(result.Data as PatientInsurenceProviderModel) : View();
        }


        public ActionResult Create() => View();

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatientDtoSave patientDtoSave)
        {
            patientDtoSave.CreatedAt = DateTime.Now;

            var result = await _patientService.SaveAsync(patientDtoSave);
            if (result.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Message = result.Message;
            return View();
        }

        
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _patientService.GetById(id);
            return result.IsSuccess ? View(result.Data as PatientInsurenceProviderModel) : View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PatientDtoUpdate patientDtoUpdate)
        {
            patientDtoUpdate.UpdatedAt = DateTime.Now;

            var result = await _patientService.UpdateAsync(patientDtoUpdate);
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
