using MedicalAppointment.Aplication.Contracts.Insurance;
using MedicalAppointment.Aplication.Dtos.Configuration.Insurnace.NetworkType;
using MedicalAppointment.Persistance.Models.Insurnaces;
using MedicalAppointmentApp.Domain.Entities.Insurance;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointment.Web.Controllers
{
    public class NetWorkController : Controller
    {

        private readonly INetworkType _networkType;

        public NetWorkController(INetworkType networkTypeRepository)
        {
            _networkType = networkTypeRepository;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _networkType.GetAll();

            if (result.IsSuccess)
            {
                List<NetworkTypeModel>networkTypes = (List<NetworkTypeModel>)result.Data;

                return View(networkTypes);
            }
            return View(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await _networkType.GetById(id);

            if (result.IsSuccess)
            {
                NetworkTypeModel networkTypes = (NetworkTypeModel)result.Data;

                return View(networkTypes);

            }
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NetworkSaveDto networkSaveDto)
        {
            try
            {
                var result = await _networkType.SaveAsync(networkSaveDto);

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
            var result = await _networkType.GetById(id);

            if (result.IsSuccess)
            {

                NetworkTypeModel network = (NetworkTypeModel)result.Data;
                return View(network);

            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NetworkUpdateDto networkUpdateDto)
        {
            try
            {
                networkUpdateDto.UpdatedAt = DateTime.Now;
                var result = await _networkType.UpdateAsync(networkUpdateDto);

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
