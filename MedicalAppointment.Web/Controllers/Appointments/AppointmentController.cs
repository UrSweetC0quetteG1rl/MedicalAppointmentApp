using MedicalAppointment.Aplication.Contracts.Appointment;
using MedicalAppointment.Aplication.Dtos.Configuration.Appointment.Appointments;
using MedicalAppointment.Persistance.Models.Appointments;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointment.Web.Controllers.Appointments
{
    public class AppointmentController : Controller
    {
        private readonly IAppointment _appointment;

        public AppointmentController(IAppointment appointmentRepository)
        {
            _appointment = appointmentRepository;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _appointment.GetAll();

            if (result.IsSuccess)
            {
                List<AppointmentDoctorModel> appointments = (List<AppointmentDoctorModel>)result.Data;

                return View(appointments);
            }
            return View(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await _appointment.GetById(id);

            if (result.IsSuccess)
            {
                AppointmentDoctorModel appointments = (AppointmentDoctorModel)result.Data;

                return View(appointments);

            }
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppointmentSaveDto appointmentSaveDto)
        {
            try
            {
                var result = await _appointment.SaveAsync(appointmentSaveDto);

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
            var result = await _appointment.GetById(id);

            if (result.IsSuccess)
            {

                AppointmentDoctorModel appointment = (AppointmentDoctorModel)result.Data;
                return View(appointment);

            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AppointmentUpdateDto appointmentUpdateDto)
        {
            try
            {

                var result = await _appointment.UpdateAsync(appointmentUpdateDto);

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
