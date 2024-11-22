using MedicalAppointmentApp.Domain.Repositories;
using MedicalAppointmentApp.Domain.Result;
using MedicalAppointmentApp.Domain.Entities.User;

namespace MedicalAppointment.Persistance.Interfaces.Users
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<OperationResult> GetUserById(int userId);
    }
}
