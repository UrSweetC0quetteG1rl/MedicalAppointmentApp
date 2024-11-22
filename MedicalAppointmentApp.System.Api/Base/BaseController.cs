using MedicalAppointmentApp.Application.Core;
using Microsoft.AspNetCore.Mvc;


namespace MedicalAppointmentApp.System.Api.Base
{
    public abstract class BaseController : ControllerBase
    {
        protected async Task<IActionResult> HandleRepositoryAction(Func<Task<BaseResponse>> action)
        {
            var result = await action();
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
