using MedicalAppointmentApp.Web.Models;

namespace MedicalAppointmentApp.Web.Base
{
    public interface IApiService<T>
    {
        Task<List<T>> GetAllAsync(string endpoint);
        Task<T> GetByIdAsync(string endpoint, int id);
        Task<BaseApiResponseModel> CreateAsync(string endpoint, T entity);
        Task<BaseApiResponseModel> UpdateAsync(string endpoint, int id, T entity);
    }
}
