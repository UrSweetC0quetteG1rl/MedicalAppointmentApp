
using MedicalAppointmentApp.Application.Dtos.Users.Patient;
using MedicalAppointmentApp.Web.Base;
using MedicalAppointmentApp.Web.Models.Users.Patient;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointmentApp.Web.Controllers
{
    public class PatientAdmController : BaseController<PatientDtoSave, PatientGetAllResultModel, PatientGetByIdModel>
    {
        public PatientAdmController() : base("http://localhost:5035/api/") { }

        public async Task<IActionResult> Index()
        {
            var model = await GetAllAsync("Patient/GetPatients");
            return View(model.data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await GetByIdAsync("Patient/GetPatientById", id);
            return View(model.data);
        }

        
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatientDtoSave patientDtoSave)
        {
            if (ModelState.IsValid)
            {
                var model = await CreateAsync("Patient/SavePatient", patientDtoSave);
                if (!model.IsSuccess)
                {
                    ViewBag.Message = $"Error al crear el paciente: {model.message}";
                    return View(patientDtoSave);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Message = "Por favor, corrija los errores en el formulario.";
            return View(patientDtoSave);

        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await GetByIdAsync("Patient/GetPatientById", id);
            return View(model.data);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PatientDtoSave patientDtoSave)
        {
            if (ModelState.IsValid)
            {
                var model = await UpdateAsync("Patient/UpdatePatient", id, patientDtoSave);
                if (!model.IsSuccess)
                {
                    ViewBag.Message = model.message;
                    return View(patientDtoSave);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(patientDtoSave);
        }


    }
}
