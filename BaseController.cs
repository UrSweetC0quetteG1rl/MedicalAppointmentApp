using MedicalAppointmentApp.Domain.Result;
using Microsoft.AspNetCore.Mvc;

public abstract class BaseController : ControllerBase
{
    protected async Task<IActionResult> HandleRepositoryAction(Func<Task<OperationResult>> action)
    {
        var result = await action();
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
}
