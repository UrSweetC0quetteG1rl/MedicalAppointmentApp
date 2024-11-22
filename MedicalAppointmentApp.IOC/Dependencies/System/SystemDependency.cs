using MedicalAppointment.Persistance.Interfaces.System;
using MedicalAppointment.Persistance.Repositories.System;
using MedicalAppointmentApp.Application.Contracts.System;
using MedicalAppointmentApp.Application.Services.System;
using Microsoft.Extensions.DependencyInjection;


namespace MedicalAppointmentApp.IOC.Dependencies.System
{
    public static class SystemDependency
    {
        public static void AddSystemDependency(this IServiceCollection services) 
        {
            //Registro de cada dependencia de repositorios
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IStatusRepository, StatusRepository>();

            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IStatusService, StatusService>();
        }
    }
}
