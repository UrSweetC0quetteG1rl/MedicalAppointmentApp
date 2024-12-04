using MedicalAppointment.Persistance.Models.Insurnaces;
using MedicalAppointment.Web.Models.Insurances.NetworkType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MedicalAppointment.Web.Controllers
{
    public class NetWorkAdmController : Controller
    {
        // GET: NetWorkAdmController
        public async Task<IActionResult> Index()
        {

            NetworkGetAllModel networkTypeModel = new NetworkGetAllModel();

            string url = "http://localhost:5138/api/";


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);

                var responseTask = await client.GetAsync("NetworkType/GetNetWorkType");

                if (responseTask.IsSuccessStatusCode)
                {
                    string response = await responseTask.Content.ReadAsStringAsync();


                    networkTypeModel = JsonConvert.DeserializeObject<NetworkGetAllModel>(response);


                }
                else
                {
                    ViewBag.Message = "Error";
                }
                
            }

            return View(networkTypeModel.Data);
        }

        // GET: NetWorkAdmController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            NetworkGetByIDModel networkTypeIdModel = new NetworkGetByIDModel();

            string url = "http://localhost:5138/api/";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);

                var responseTask = await client.GetAsync($"NetworkType/{id}");


                if (responseTask.IsSuccessStatusCode)
                {
                    string response = await responseTask.Content.ReadAsStringAsync();

                    networkTypeIdModel = JsonConvert.DeserializeObject<NetworkGetByIDModel>(response);


                }

            }
            return View(networkTypeIdModel.data);

        }

        // GET: NetWorkAdmController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NetWorkAdmController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NetWorkAdmController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NetWorkAdmController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NetWorkAdmController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NetWorkAdmController/Delete/5
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
        }
    }
}
