using MedicalAppointment.Persistance.Context;
using MedicalAppointment.Persistance.Interfaces.Configuration.Medical;
using MedicalAppointment.Persistance.Repositories.Configuration.Medical;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<MedicalAppointmentContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("AppointmentDb")));


builder.Services.AddScoped<IAvailabilityModesRepository, AvailabilityModesRepository>();
builder.Services.AddScoped<IMedicalRecordsRepository, MedicalRecordRepository>();
builder.Services.AddScoped<ISpecialtiesRepository, SpecialtiesRepository>();


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
