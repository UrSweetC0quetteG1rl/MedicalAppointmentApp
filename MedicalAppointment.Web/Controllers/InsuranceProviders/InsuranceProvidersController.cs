using MedicalAppointment.Aplication.Contracts.Insurance;
using MedicalAppointment.Aplication.Dtos.Configuration.Insurnace.InsuranceProviders;
using MedicalAppointment.Persistance.Models.Insurnaces;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointment.Web.Controllers.InsuranceProviders
{
    public class InsuranceProvidersController : Controller
    {

        private readonly IInsuranceProviders _insuranceProviders;


        public InsuranceProvidersController(IInsuranceProviders insuranceProvidersRepository)
        {
            _insuranceProviders = insuranceProvidersRepository;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _insuranceProviders.GetAll();

            if (result.IsSuccess)
            {
                List<InsuranceProvidersNetworkModel> insurances = (List<InsuranceProvidersNetworkModel>)result.Data;

                return View(insurances);

            }
            return View();

        }


        public async Task<IActionResult> Details(int id)
        {
            var result = await _insuranceProviders.GetById(id);

            if (result.IsSuccess)
            {
                InsuranceProvidersNetworkModel insurances = (InsuranceProvidersNetworkModel)result.Data;

                return View(insurances);

            }
            return View();
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InsuranceProvidersSaveDto providersSaveDto)
        {
            try
            {
                var result = await _insuranceProviders.SaveAsync(providersSaveDto);

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
            var result = await _insuranceProviders.GetById(id);

            if (result.IsSuccess)
            {
                InsuranceProvidersNetworkModel insurances = (InsuranceProvidersNetworkModel)result.Data;

                return View(insurances);

            }

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(InsuranceProvidersUpdateDto providersUpdateDto)
        {
            try
            {
                providersUpdateDto.UpdatedAt = DateTime.Now;

                var result = await _insuranceProviders.UpdateAsync(providersUpdateDto);


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



    }
}
