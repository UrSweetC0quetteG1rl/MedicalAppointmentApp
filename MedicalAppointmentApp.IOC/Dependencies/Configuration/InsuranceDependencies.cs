using MedicalAppointment.Aplication.Contracts;
using MedicalAppointment.Aplication.Services.Configuration;
using MedicalAppointment.Persistance.Interfaces.Configuration.Insurance;
using MedicalAppointment.Persistance.Repositories.Configuration.Insurance;
using Microsoft.Extensions.DependencyInjection;


namespace MedicalAppointmentApp.IOC.Dependencies.Configuration
{
    public static class InsuranceDependencies
    {

        public static void AddInsuranceDependencies(this IServiceCollection services)
        {

            services.AddScoped<IInsuranceProvidersRepository, InsuranceProvidersRepository>();

            services.AddScoped<INetworkTypeRepository, NetworkTypeRepository>();

            services.AddTransient<IInsuranceProviders, InsuranceProviderService>();


        }

    }
}
