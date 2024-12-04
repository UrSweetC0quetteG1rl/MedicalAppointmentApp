using MedicalAppointment.Aplication.Dtos.Configuration.Appointment.Appointments;
using MedicalAppointment.Web.Models.Appointments.Appointment;
using MedicalAppointment.Web.Models.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace MedicalAppointment.Web.Controllers.Appointments
{
    public class AppointmentAdmController : Controller
    {
        private readonly string _baseUrl = "http://localhost:5150/api/";

        private async Task<T> SendHttpRequestAsync<T>(string endpoint, HttpMethod method, object? data = null)
        {
            using var client = new HttpClient { BaseAddress = new Uri(_baseUrl) };
            var request = new HttpRequestMessage(method, endpoint);

            if (data != null)
            {
                request.Content = JsonContent.Create(data);
            }

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(responseString);
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var model = await SendHttpRequestAsync<AppointmentGetAllModel>("Appointment/GetAppointment", HttpMethod.Get);
                return View(model.Data);
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Error: {ex.Message}";
                return View(new List<object>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var model = await SendHttpRequestAsync<AppointmentGetByID>($"Appointment/{id}", HttpMethod.Get);
                return View(model.data);
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Error: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppointmentSaveDto appointmentSaveDto)
        {
            try
            {
                var response = await SendHttpRequestAsync<BaseApiModel>("Appointment/SavesAppointment", HttpMethod.Post, appointmentSaveDto);


                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Error: {ex.Message}";
                return View();
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var model = await SendHttpRequestAsync<AppointmentGetByID>($"Appointment/{id}", HttpMethod.Get);
                return View(model.data);
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Error: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AppointmentUpdateDto appointmentUpdate)
        {
            try
            {
                var response = await SendHttpRequestAsync<BaseApiModel>("Appointment/UpdateAppointment", HttpMethod.Post, appointmentUpdate);


                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Error: {ex.Message}";
                return View();
            }
        }
    }
}
