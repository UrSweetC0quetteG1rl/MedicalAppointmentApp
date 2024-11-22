using MedicalAppointmentApp.Application.Dtos.Users.Doctor;
using MedicalAppointmentApp.Web.Base;
using MedicalAppointmentApp.Web.Models.Users.Doctor;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointmentApp.Web.Controllers
{
    public class DoctorAdmController : BaseController<DoctorDtoSave, DoctorGetAllResultModel, DoctorGetByIdModel>
    {
        public DoctorAdmController() : base("http://localhost:5035/api/") { }

        public async Task<IActionResult> Index()
        {
            var model = await GetAllAsync("Doctor/GetDoctors");
            return View(model.data);
        }


        public async Task<IActionResult> Details(int id)
        {
            var model = await GetByIdAsync("Doctor/GetDoctorById", id);
            return View(model.data);
        }


        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DoctorDtoSave doctorDtoSave)
        {
            if (ModelState.IsValid)
            {
                var model = await CreateAsync("Doctor/SaveDoctor", doctorDtoSave);
                if (!model.IsSuccess)
                {
                    ViewBag.Message = $"Error al crear el doctor: {model.message}";
                    return View(doctorDtoSave);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Message = "Por favor, corrija los errores en el formulario.";
            return View(doctorDtoSave);

        }


        public async Task<IActionResult> Edit(int id)
        {
            var model = await GetByIdAsync("Doctor/GetDoctorById", id);
            return View(model.data);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DoctorDtoSave doctorDtoSave)
        {
            if (ModelState.IsValid)
            {
                var model = await UpdateAsync("Doctor/UpdateDoctor", id, doctorDtoSave);
                if (!model.IsSuccess)
                {
                    ViewBag.Message = model.message;
                    return View(doctorDtoSave); 
                }
                return RedirectToAction(nameof(Index));
            }
            return View(doctorDtoSave); 
        }



    }
}
