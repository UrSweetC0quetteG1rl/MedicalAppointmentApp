using MedicalAppointment.Aplication.Dtos.Configuration.Insurnace.InsuranceProviders;
using MedicalAppointment.Persistance.Models.Insurnaces;
using MedicalAppointment.Web.Models.Core;
using MedicalAppointment.Web.Models.Insurances.InsurenceProviders;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointment.Web.Controllers.InsuranceProviders
{
    public class InsuranceProvidersAdmController : BaseApiController
    {
        public InsuranceProvidersAdmController(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var insuranceGetAllModel = await SendHttpRequestAsync<InsuranceGetAllModel>("InsuranceProviders/GetInsutanceProviders", HttpMethod.Get);

                if (insuranceGetAllModel == null || insuranceGetAllModel.Data == null)
                {
                    ViewBag.Message = "No data available.";
                    return View(new List<InsuranceProvidersNetworkModel>());
                }

                return View(insuranceGetAllModel.Data);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(new List<InsuranceProvidersNetworkModel>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var model = await SendHttpRequestAsync<InsuranceProvidersGetByIDModel>($"InsuranceProviders/{id}", HttpMethod.Get);
                return View(model.data);
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Error: {ex.Message}";
                return View("Error");
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InsuranceProvidersSaveDto providersSaveDto)
        {
            try
            {
                var response = await SendHttpRequestAsync<BaseApiModel>("InsuranceProviders/SavesInsurance", HttpMethod.Post, providersSaveDto);


                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var model = await SendHttpRequestAsync<InsuranceProvidersGetByIDModel>($"InsuranceProviders/{id}", HttpMethod.Get);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Error: {ex.Message}";
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(InsuranceProvidersUpdateDto insuranceProvidersUpdate)
        {
            try
            {
                var response = await SendHttpRequestAsync<BaseApiModel>("InsuranceProviders/UpdateInsurance", HttpMethod.Post, insuranceProvidersUpdate);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"An error occurred: {ex.Message}";
                return View();
            }
        }

    }
}
