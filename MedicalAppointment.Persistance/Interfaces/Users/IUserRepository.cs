using MedicalAppointmentApp.Domain.Repositories;
using MedicalAppointmentApp.Domain.Result;
using MedicalAppointmentApp.Domain.Entities.User;

namespace MedicalAppointment.Persistance.Interfaces.Users
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<OperationResult> GetUserByEmailAndPassword(string email, string password);
        Task<OperationResult> ConfirmUserRegistration(string email);
        Task<OperationResult> ForgotPassword(string email);
        Task<OperationResult> IsEmailInUse(string email);
    }
}
