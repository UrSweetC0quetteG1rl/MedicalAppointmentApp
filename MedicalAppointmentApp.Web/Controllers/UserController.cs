using MedicalAppointment.Persistance.Models.User;
using MedicalAppointmentApp.Application.Contracts.Users;
using MedicalAppointmentApp.Application.Dtos.Users.User;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointmentApp.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult>  Index()
        {
            var result = await _userService.GetAll();

            return result.IsSuccess ? View(result.Data as List<UserRoleModel>) : View();
        }

        
        public async Task<IActionResult> Details(int id)
        {
            var result = await _userService.GetById(id);
            return result.IsSuccess ? View(result.Data as UserRoleModel) : View();
        }


        public ActionResult Create() => View();

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserDtoSave userDtoSave)
        {
            userDtoSave.CreatedAt = DateTime.Now;

            var result = await _userService.SaveAsync(userDtoSave);
            if (result.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Message = result.Message;
            return View();
        }

        
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _userService.GetById(id);
            return result.IsSuccess ? View(result.Data as UserRoleModel) : View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserDtoUpdate userDtoUpdate)
        {
            userDtoUpdate.UpdatedAt = DateTime.Now;

            var result = await _userService.UpdateAsync(userDtoUpdate);
            if (result.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Message = result.Message;
            return View();
        }

        /*
        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
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
        }*/
    }
}
