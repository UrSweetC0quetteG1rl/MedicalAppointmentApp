using MedicalAppointment.Aplication.Dtos.Configuration.Insurnace.InsuranceProviders;
using MedicalAppointment.Web.Models.Core;
using MedicalAppointment.Web.Models.Insurances.InsurenceProviders;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointment.Web.Controllers
{
    public class InsuranceProvidersAdmController : BaseApiController
    {
        public InsuranceProvidersAdmController() : base("http://localhost:5138/api/")
        {
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var model = await SendHttpRequestAsync<InsuranceGetAllModel>("InsuranceProviders/GetInsutanceProviders", HttpMethod.Get);
                return View(model.Data);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
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
                return HandleError(ex);
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
                if (!string.IsNullOrEmpty(response.Message))
                {
                    ViewBag.Message = response.Message;
                    return View();
                }

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
                return View(model.data);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]       
        public async Task<IActionResult> Edit(InsuranceProvidersUpdateDto insuranceProvidersUpdate)
        {
            try
            {
                var response = await SendHttpRequestAsync<BaseApiModel>("InsuranceProviders/UpdateInsurance", HttpMethod.Post, insuranceProvidersUpdate);

                if (response == null)
                {
                    ViewBag.Message = "La respuesta fue nula.";
                    return View();
                }

                if (!string.IsNullOrEmpty(response.Message))
                {
                    ViewBag.Message = response.Message;
                    return View();
                }

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
