using MedicalAppointment.Aplication.Contracts.Appointment;
using MedicalAppointment.Aplication.Dtos.Configuration.Appointment.DoctorAvailability;
using MedicalAppointment.Aplication.Dtos.Configuration.Insurnace.NetworkType;
using MedicalAppointment.Persistance.Models.Appointments;
using MedicalAppointment.Persistance.Models.Insurnaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointment.Web.Controllers
{
    public class DoctorAvailabilityController : Controller
    {
        private readonly IDoctorAvailability _doctoravailability;

        public DoctorAvailabilityController(IDoctorAvailability doctoravailability)
        {
            _doctoravailability = doctoravailability;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _doctoravailability.GetAll();

            if (result.IsSuccess)
            {
                List<DoctorAvailabilityModel> doctorsavaila = (List<DoctorAvailabilityModel>)result.Data;

                return View(doctorsavaila);
            }
            return View(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await _doctoravailability.GetById(id);

            if (result.IsSuccess)
            {
                DoctorAvailabilityModel doctorsavaila = (DoctorAvailabilityModel)result.Data;

                return View(doctorsavaila);

            }
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DoctorAvailabilitySaveDto doctorAvailabilitySaveDto)
        {
            try
            {
                var result = await _doctoravailability.SaveAsync(doctorAvailabilitySaveDto);

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
        }

        public async Task<IActionResult> Edit(int id)
        {
            var result = await _doctoravailability.GetById(id);

            if (result.IsSuccess)
            {

                DoctorAvailabilityModel doctorsavaila = (DoctorAvailabilityModel)result.Data;
                return View(doctorsavaila);

            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DoctorAvailabilityUpdateDto doctorAvailabilityUpdateDto)
        {
            try
            {
                var result = await _doctoravailability.UpdateAsync(doctorAvailabilityUpdateDto);

                if (result.IsSuccess)
                {
                    ViewBag.Message = result.Message;
                    return View();
                }


                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();
            }
        }


    }
}
