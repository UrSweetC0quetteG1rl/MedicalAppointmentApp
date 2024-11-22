

using MedicalAppointment.Persistance.Interfaces.Users;
using MedicalAppointment.Persistance.Repositories.UserRepository;
using MedicalAppointment.Persistance.Repositories.Users;
using MedicalAppointmentApp.Application.Contracts.Users;
using MedicalAppointmentApp.Application.Services.Users;
using Microsoft.Extensions.DependencyInjection;

namespace MedicalAppointmentApp.IOC.Dependencies.User
{
    public static class UserDependency
    {
        public static void AddUserDependency(this IServiceCollection service)
        {
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IDoctorRepository, DoctorRepository>();
            service.AddScoped<IPatientRepository, PatientRepository>();

            service.AddTransient<IUserService, UserService>();
            service.AddTransient<IDoctorService, DoctorService>();
            service.AddTransient<IPatientService, PatientService>();

        }
    }
}
