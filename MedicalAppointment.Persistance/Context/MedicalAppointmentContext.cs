

using MedicalAppointmentApp.Domain.Entities.Appoinments;
using MedicalAppointmentApp.Domain.Entities.Insurance;
using MedicalAppointmentApp.Domain.Entities.Medical;
using MedicalAppointmentApp.Domain.Entities.System;
using MedicalAppointmentApp.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
//hace conexion con base de datos

namespace MedicalAppointment.Persistance.Context
{
    public partial class MedicalAppointmentContext: DbContext
    {
        public MedicalAppointmentContext(DbContextOptions<MedicalAppointmentContext> options)
            : base(options)
        { }

        #region"Appointment Entities"
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<DoctorAvailability> DoctorAvailability { get; set; }
        #endregion
        //dbset se encarga de hacer mapeo entre entidad y la base de datos

        #region"Insurance Entities"
        public DbSet<InsuranceProvider> InsuranceProviders { get; set; }
        public DbSet<NetworkType> NetworkType { get; set; }
        #endregion


        #region"Medical Entities"
        public DbSet<AvailabilityMode> AvailabilityModes { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        #endregion


        #region"System Entities"
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Status> Status { get; set; }
        #endregion


        #region"User Entities"
        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Employee> Employees { get; set; }
        #endregion
       

    }
}
