using Azure;
using MedicalAppointment.Aplication.Dtos.Configuration.Insurnace.NetworkType;
using MedicalAppointment.Persistance.Models.Insurnaces;
using MedicalAppointment.Web.Models.Core;
using MedicalAppointment.Web.Models.Insurances.NetworkType;
using Microsoft.AspNetCore.Mvc;

public class NetWorkAdmController : BaseApiController
{
    public NetWorkAdmController(IHttpClientFactory httpClientFactory) : base(httpClientFactory) { }

    public async Task<IActionResult> Index()
    {
        var networkTypeModel = await SendHttpRequestAsync<NetworkGetAllModel>("NetworkType/GetNetWorkType", HttpMethod.Get);

        if (networkTypeModel == null || networkTypeModel.Data == null)
        {
            ViewBag.Message = "No data available.";
            return View(new List<NetworkTypeModel>());
        }

        return View(networkTypeModel.Data);
    }

    public async Task<IActionResult> Details(int id)
    {
        var networkTypeIdModel = await SendHttpRequestAsync<NetworkGetByIDModel>($"NetworkType/{id}", HttpMethod.Get);

        if (networkTypeIdModel == null)
        {
            return NotFound();
        }

        return View(networkTypeIdModel.data);
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(NetworkSaveDto networkSave)
    {
        var response = await SendHttpRequestAsync<BaseApiModel>("NetworkType/SavesNet", HttpMethod.Post, networkSave);

        

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var networkTypeIdModel = await SendHttpRequestAsync<NetworkGetByIDModel>($"NetworkType/{id}", HttpMethod.Get);

        if (networkTypeIdModel == null)
        {
            return NotFound();
        }

        return View(networkTypeIdModel.data);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(NetworkUpdateDto networkUpdateDto)
    {
        var model = await SendHttpRequestAsync<BaseApiModel>("NetworkType/UpdateNetwork", HttpMethod.Post, networkUpdateDto);
        

        return RedirectToAction(nameof(Index));
    }
}
