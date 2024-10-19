using MedicalAppointment.Persistance.Context;
using MedicalAppointment.Persistance.Interfaces.Configuration.Insurance;
using MedicalAppointment.Persistance.Repositories.Configuration.Insurance;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<MedicalAppointmentContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("AppointmentDb")));

//El Registro de cada dependencia Repositorios.//

builder.Services.AddScoped<IInsuranceProvidersRepository, InsuranceProvidersRepository>();
builder.Services.AddScoped<INetworkTypeRepository, NetworkTypeRepository>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
