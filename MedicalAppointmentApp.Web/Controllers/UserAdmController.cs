using MedicalAppointmentApp.Application.Dtos.Users.User;
using MedicalAppointmentApp.Web.Base;
using MedicalAppointmentApp.Web.Models.Users.Users;
using Microsoft.AspNetCore.Mvc;


namespace MedicalAppointmentApp.Web.Controllers
{
    public class UserAdmController : BaseController<UserDtoSave, UserGetAllResultModel, UserGetByIdModel>
    {
        public UserAdmController() : base("http://localhost:5035/api/") { }

        public async Task<IActionResult> Index()
        {
            var model = await GetAllAsync("User/GetUsers");
            return View(model.data);
        }


        public async Task<IActionResult> Details(int id)
        {
            var model = await GetByIdAsync("User/GetUserById", id);
            return View(model.data);
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserDtoSave userDtoSave)
        {
            if (ModelState.IsValid)
            {
                var model = await CreateAsync("User/SaveUser", userDtoSave);
                if (!model.IsSuccess)
                {
                    ViewBag.Message = $"Error al crear el usuario: {model.message}";
                    return View(userDtoSave);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Message = "Por favor, corrija los errores en el formulario.";
            return View(userDtoSave);

        }


        public async Task<IActionResult> Edit(int id)
        {
            var model = await GetByIdAsync("User/GetUserById", id);
            return View(model.data);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserDtoSave userDtoSave)
        {
            if (ModelState.IsValid)
            {
                var model = await UpdateAsync("User/UpdateUser", id, userDtoSave);
                if (!model.IsSuccess)
                {
                    ViewBag.Message = model.message;
                    return View(userDtoSave); 
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userDtoSave); 
        }


    }
}