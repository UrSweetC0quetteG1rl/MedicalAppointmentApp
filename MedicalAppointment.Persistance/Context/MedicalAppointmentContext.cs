

using MedicalAppointmentApp.Domain.Entities.Appoinments;
using MedicalAppointmentApp.Domain.Entities.Insurance;
using MedicalAppointmentApp.Domain.Entities.Medical;
using MedicalAppointmentApp.Domain.Entities.System;
using MedicalAppointmentApp.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace MedicalAppointment.Persistance.Context
{
    public class MedicalAppointmentContext: DbContext
    {
        public MedicalAppointmentContext(DbContextOptions<MedicalAppointmentContext> options)
            : base(options)
        { 
        
        }

        #region"Appointments Entities"
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<DoctorAvailability> DoctorAvailability { get; set; }
        #endregion


        #region"Insurance Entities"
        public DbSet<InsuranceProviders> InsuranceProviders { get; set; }
        public DbSet<NetworkType> NetworkType { get; set; }
        #endregion


        #region"Medical Entities"
        public DbSet<AvailabilityModes> AvailabilityModes { get; set; }
        public DbSet<MedicalRecords> MedicalRecords { get; set; }
        public DbSet<Specialties> Specialties { get; set; }
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
        public DbSet<Employee> Employee { get; set; }
        #endregion



    }
}
