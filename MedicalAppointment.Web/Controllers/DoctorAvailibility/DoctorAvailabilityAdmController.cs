using MedicalAppointment.Aplication.Dtos.Configuration.Appointment.DoctorAvailability;
using MedicalAppointment.Aplication.Dtos.Configuration.Insurnace.NetworkType;
using MedicalAppointment.Web.Models.Appointments.DoctorAvailability;
using MedicalAppointment.Web.Models.Core;
using MedicalAppointment.Web.Models.Insurances.NetworkType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace MedicalAppointment.Web.Controllers.DoctorAvailibility
{
    public class DoctorAvailabilityAdmController : Controller
    {
        public async Task<IActionResult> Index()
        {

            DoctorAvailabilityGetAllModel doctorAvailabilityGetAll = new DoctorAvailabilityGetAllModel();

            string url = "http://localhost:5150/api/";


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);

                var responseTask = await client.GetAsync("DoctorAvailability/GetDoctorAvailability");

                if (responseTask.IsSuccessStatusCode)
                {
                    string response = await responseTask.Content.ReadAsStringAsync();


                    doctorAvailabilityGetAll = JsonConvert.DeserializeObject<DoctorAvailabilityGetAllModel>(response);


                }
                else
                {
                    ViewBag.Message = "Error";
                }

            }

            return View(doctorAvailabilityGetAll.Data);
        }

        public async Task<IActionResult> Details(int id)
        {
            DoctorAvailabilityGetByID doctorAvailabilityGetAll = new DoctorAvailabilityGetByID();

            string url = "http://localhost:5150/api/";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);

                var responseTask = await client.GetAsync($"DoctorAvailability/{id}");


                if (responseTask.IsSuccessStatusCode)
                {
                    string response = await responseTask.Content.ReadAsStringAsync();

                    doctorAvailabilityGetAll = JsonConvert.DeserializeObject<DoctorAvailabilityGetByID>(response);


                }

            }
            return View(doctorAvailabilityGetAll.data);

        }

        public ActionResult Create()
        {
            return View();
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(DoctorAvailabilitySaveDto doctorAvailabilitySave)
		{
			BaseApiModel model = new BaseApiModel();
			try
			{
				string url = "http://localhost:5150/api/";

				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(url);

					var responseTask = await client.PostAsJsonAsync<DoctorAvailabilitySaveDto>("DoctorAvailability/SavesAvailability", doctorAvailabilitySave);

					if (responseTask.IsSuccessStatusCode)
					{
						string response = await responseTask.Content.ReadAsStringAsync();

						model = JsonConvert.DeserializeObject<BaseApiModel>(response);


					}
					else
					{
						string response = await responseTask.Content.ReadAsStringAsync();

						model = JsonConvert.DeserializeObject<BaseApiModel>(response);
						ViewBag.Message = model.Message;
						return View();

					}
				}
				return RedirectToAction(nameof(Index));


			}
			catch
			{
				return View();
			}
		}

		public async Task<IActionResult> Edit(int id)
		{

			DoctorAvailabilityGetByID doctorAvailabilityGetBy = new DoctorAvailabilityGetByID();

			string url = "http://localhost:5150/api/";

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(url);

				var responseTask = await client.GetAsync($"NetworkType/{id}");


				if (responseTask.IsSuccessStatusCode)
				{
					string response = await responseTask.Content.ReadAsStringAsync();

					doctorAvailabilityGetBy = JsonConvert.DeserializeObject<DoctorAvailabilityGetByID>(response);


				}

			}
			return View(doctorAvailabilityGetBy.data);
		}

		[HttpPost]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(DoctorAvailabilityUpdateDto doctorAvailabilityUpdate)
		{
			BaseApiModel model = new BaseApiModel();
			try
			{
				string url = "http://localhost:5150/api/";

				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(url);

					var responseTask = await client.PostAsJsonAsync<DoctorAvailabilityUpdateDto>("DoctorAvailability/UpdateAvailability", doctorAvailabilityUpdate);

					if (responseTask.IsSuccessStatusCode)
					{
						string response = await responseTask.Content.ReadAsStringAsync();

						model = JsonConvert.DeserializeObject<BaseApiModel>(response);


					}
					else
					{
						string response = await responseTask.Content.ReadAsStringAsync();

						model = JsonConvert.DeserializeObject<BaseApiModel>(response);
						ViewBag.Message = model.Message;
						return View();

					}
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
