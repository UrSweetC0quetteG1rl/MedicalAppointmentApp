
namespace MedicalAppointmentApp.Application.Base
{
    public interface IBaseService<TResponse, TSaveDto, TUpdateDto>
    {
        Task<TResponse> SaveAsync(TSaveDto dto);
        Task<TResponse> UpdateAsync(TUpdateDto dto);
        Task<TResponse> GetAll();
        Task<TResponse> GetById(int Id);
        
    }
}
