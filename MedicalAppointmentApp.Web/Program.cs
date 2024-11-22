using MedicalAppointment.Persistance.Context;
using MedicalAppointmentApp.Application.Dtos.Users.User;
using MedicalAppointmentApp.IOC.Dependencies.System;
using MedicalAppointmentApp.IOC.Dependencies.User;
using MedicalAppointmentApp.Web.Base;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MedicalAppointmentContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MedicalAppointmentDb")));

//registro cada dependencia repositorio de configuration
builder.Services.AddUserDependency();
builder.Services.AddSystemDependency();

//SERVICIOS PARA LA API
/*builder.Services.AddHttpClient<IApiService<UserDtoSave>, ApiService<UserDtoSave>>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5035/api/"); // Cambia esto a la URL de tu API
});*/

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
