using MedicalAppointment.Aplication.Dtos.Configuration.Insurnace.NetworkType;
using MedicalAppointment.Persistance.Models.Insurnaces;
using MedicalAppointment.Web.Models.Core;
using MedicalAppointment.Web.Models.Insurances.NetworkType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Security.Policy;

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
        public async Task<IActionResult> Create(NetworkSaveDto networkSave)
        {
            BaseApiModel model = new BaseApiModel();
            try
            {
                string url = "http://localhost:5138/api/";

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);

                    var responseTask = await client.PostAsJsonAsync<NetworkSaveDto>("NetworkType/SavesNet", networkSave);

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

        // GET: NetWorkAdmController/Edit/5
        public async Task<IActionResult> Edit(int id)
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

        // POST: NetWorkAdmController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NetworkUpdateDto networkUpdateDto )
        {
            BaseApiModel model = new BaseApiModel();
            try
            {
                string url = "http://localhost:5138/api/";

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);

                    var responseTask = await client.PostAsJsonAsync<NetworkUpdateDto>("NetworkType/UpdateNetwork", networkUpdateDto);

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
