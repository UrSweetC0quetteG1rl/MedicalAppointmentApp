using MedicalAppointment.Persistance.Interfaces.Configuration.Appointments;
using MedicalAppointment.Persistance.Repositories.Configuration.Appointments;
using Microsoft.Extensions.DependencyInjection;


namespace MedicalAppointmentApp.IOC.Dependencies.Configuration
{
    public static class AppointmentDependencies
    {
        public static void AddAppointmentDependencies(this IServiceCollection services)
        {
           
            services.AddScoped<IMedicalAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IDoctorAvailabilityRepository, DoctorAvailabilityRepository>();

            


        }
    }
}
