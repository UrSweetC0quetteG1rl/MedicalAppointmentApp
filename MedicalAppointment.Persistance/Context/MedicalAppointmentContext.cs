

using MedicalAppointmentApp.Domain.Entities.Appoinments;
using MedicalAppointmentApp.Domain.Entities.Insurance;
using MedicalAppointmentApp.Domain.Entities.Medical;
using MedicalAppointmentApp.Domain.Entities.System;
using MedicalAppointmentApp.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;


namespace MedicalAppointment.Persistance.Context
{
    public partial class MedicalAppointmentContext: DbContext
    {
        public MedicalAppointmentContext(DbContextOptions<MedicalAppointmentContext> options)
            : base(options)
        { }

        #region"Appointment Entities"
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<DoctorAvailability> DoctorAvailability { get; set; }
        #endregion


        #region"Insurance Entities"
        public DbSet<InsuranceProvider> InsuranceProvider { get; set; }
        public DbSet<NetworkType> NetworkType { get; set; }
        #endregion


        #region"Medical Entities"
        public DbSet<AvailabilityMode> AvailabilityMode { get; set; }
        public DbSet<MedicalRecord> MedicalRecord { get; set; }
        public DbSet<Specialty> Specialtie { get; set; }
        #endregion


        #region"System Entities"
        public DbSet<Notification> Notification { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Status> Status { get; set; }
        #endregion


        #region"User Entities"
        public DbSet<User> User { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Employee> Employee { get; set; }
        #endregion



    }
}
