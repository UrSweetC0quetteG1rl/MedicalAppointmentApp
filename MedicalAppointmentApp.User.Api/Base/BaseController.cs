
using MedicalAppointmentApp.Domain.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace MedicalAppointmentApp.System.Api.Base
{
    
    public abstract class BaseController : ControllerBase
    {
        protected async Task<IActionResult> HandleRepositoryAction(Func<Task<OperationResult>> action)
        {
            try
            {
                var result = await action();
                if (!result.Success)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, new { message = "Ocurrió un error al acceder a la base de datos.", details = dbEx.InnerException?.Message });
            }
            catch (Exception ex) 
            {
                return StatusCode(500, new { message = "Ocurrió un error inesperado.", details = ex.Message });
            }
           
        }
    }
}
