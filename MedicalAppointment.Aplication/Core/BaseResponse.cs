

namespace MedicalAppointment.Aplication.Core
{
    public abstract class BaseResponse
    {
       protected BaseResponse() 
        {
        IsSuccess = true; 
        
        }


        public bool IsSuccess { get; set; }
        public string? Message { get; set; } //it could be a list

    }
}
