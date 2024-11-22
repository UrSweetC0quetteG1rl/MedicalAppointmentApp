using MedicalAppointment.Persistance.Base;
using MedicalAppointment.Persistance.Context;
using MedicalAppointment.Persistance.Interfaces.Users;
using MedicalAppointment.Persistance.Models.User;
using MedicalAppointmentApp.Domain.Entities.User;
using MedicalAppointmentApp.Domain.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace MedicalAppointment.Persistance.Repositories.Users
{
    public class DoctorRepository(MedicalAppointmentContext medicalAppointmentContext, ILogger<DoctorRepository> logger) 
        : BaseRepository<Doctor>(medicalAppointmentContext), IDoctorRepository
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext = medicalAppointmentContext;
        private readonly ILogger<DoctorRepository> _logger = logger;

        private OperationResult ValidateDoctorEntity(Doctor entity)
        {
            OperationResult operationResult = new OperationResult();
            if (entity == null)
            {
                operationResult.Success = false;
                operationResult.Message = "La entidad es requerida.";
                return operationResult;
            }

            if (string.IsNullOrEmpty(entity.PhoneNumber))
            {
                operationResult.Success = false;
                operationResult.Message = "El numero de telefono es requerido.";
                return operationResult;

            }

            if (entity.SpecialtyID == 0)
            {
                operationResult.Success = false;
                operationResult.Message = "La especialidad es necesaria.";
                return operationResult;
            }

            if (string.IsNullOrEmpty(entity.LicenseNumber))
            {
                operationResult.Success = false;
                operationResult.Message = "El número de licencia es necesario.";
                return operationResult;
            }
            if (entity.YearsOfExperience <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = "Llenar el campo años de experiencia es necesario.";
                return operationResult;
            }
            if (entity.LicenseExpirationDate == null)
            {
                operationResult.Success = false;
                operationResult.Message = "Es necesario que indique cuando expira su licencia medica.";
                return operationResult;
            }
            operationResult.Success = true;
            return operationResult;
        }

        public async override Task<OperationResult> Save(Doctor entity)
        {
            OperationResult operationResult = ValidateDoctorEntity(entity);

            if (!operationResult.Success) return operationResult;

            try
            {
                operationResult = await base.Save(entity);
            }
            catch (Exception ex)
            {
                operationResult = HandleException("Ocurrió un error al guardar.", ex);
            }
            return operationResult;
        }
        public async override Task<OperationResult> Update(Doctor entity)
        {
            OperationResult operationResult = ValidateDoctorEntity(entity);
            if (!operationResult.Success) return operationResult;

            try
            {
                var doctorToUpdate = await _medicalAppointmentContext.Doctors.FindAsync(entity.DoctorID);
                if (doctorToUpdate == null)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Este usuario no existe."
                    };
                }
                UpdateDoctorProperties(doctorToUpdate, entity);
                operationResult = await base.Update(doctorToUpdate);
            }
            catch (Exception ex)
            {
                operationResult = HandleException("Error actualizando el doctor.", ex);
            }
            return operationResult;
        }
        public async override Task<OperationResult> Remove(Doctor entity)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                Doctor? doctorToRemove = await _medicalAppointmentContext.Doctors.FindAsync(entity.DoctorID);

                if (doctorToRemove == null)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "El doctor no existe."
                    };
                }

                doctorToRemove.IsActive = false;
                doctorToRemove.UpdatedAt = DateTime.Now;

                operationResult = await base.Update(doctorToRemove);
            }
            catch (Exception ex)
            {
                operationResult = HandleException("Error desactivando el doctor.", ex);
            }
            return operationResult;
        }
        public async override Task<OperationResult> GetAll()
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                
                var GetDoctorWithSpecialtyAndAvailability = await GetDoctorWithSpecialtyAndAvailabilityQuery().ToListAsync();

                operationResult.Success = true;
                operationResult.Data = GetDoctorWithSpecialtyAndAvailability;
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error obteniendo los doctores.";
                _logger.LogError(operationResult.Message, ex.ToString());
            }

            return operationResult;

        }
        public async override Task<OperationResult> GetEntityBy(int Id)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                // Busca el usuario por ID junto con su rol.
                var GetDoctorWithSpecialtyAndAvailability = await GetDoctorWithSpecialtyAndAvailabilityQuery()
                                         .FirstOrDefaultAsync(doctor => doctor.DoctorID == Id);

                if (GetDoctorWithSpecialtyAndAvailability != null)
                {
                    operationResult.Success = true;
                    operationResult.Data = GetDoctorWithSpecialtyAndAvailability;
                }
                else
                {
                    operationResult.Success = false;
                    operationResult.Message = "Doctor no encontrado.";
                }
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error obteniendo el doctor.";
                _logger.LogError(operationResult.Message, ex.ToString());
            }

            return operationResult;
        }

        


        public async Task<OperationResult> DeactivateDoctorById(int doctorId)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var doctor = await _medicalAppointmentContext.Doctors.FindAsync(doctorId);
                if (doctor == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "Doctor no encontrado.";
                    return operationResult;
                }

                doctor.IsActive = false;
                doctor.UpdatedAt = DateTime.Now;

                await _medicalAppointmentContext.SaveChangesAsync();

                operationResult.Success = true;
                operationResult.Message = "Doctor desactivado exitosamente.";
            }
            catch (Exception ex)
            {
                operationResult = HandleException("Error desactivando el doctor.", ex);
            }

            return operationResult;
        }
        public async Task<OperationResult> GetDoctorsBySpecialty(short specialtyId)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var doctors = await GetDoctorWithSpecialtyAndAvailabilityQuery()
                    .Where(d => d.SpecialtyID == specialtyId)
                    .ToListAsync();

                operationResult.Success = true;
                operationResult.Data = doctors;
            }
            catch (Exception ex)
            {
                operationResult = HandleException("Error obteniendo los doctores por especialidad.", ex);
            }

            return operationResult;
        }

        private void UpdateDoctorProperties(Doctor target, Doctor source)
        {

            target.SpecialtyID = source.SpecialtyID;
            target.LicenseNumber = source.LicenseNumber;
            target.PhoneNumber = source.PhoneNumber;
            target.YearsOfExperience = source.YearsOfExperience;
            target.Education = source.Education;
            target.Bio = source.Bio;
            target.ConsultationFee = source.ConsultationFee;
            target.ClinicAddress = source.ClinicAddress;
            target.AvailabilityModeId = source.AvailabilityModeId;
            target.LicenseExpirationDate = source.LicenseExpirationDate;
            target.UpdatedAt = source.UpdatedAt;
        }
        private OperationResult HandleException(string message, Exception ex)
        {
            _logger.LogError(message, ex.ToString());
            return new OperationResult { Success = false, Message = message };
        }
        
        private IQueryable<DoctorSpecialtyAvailabilityModel> GetDoctorWithSpecialtyAndAvailabilityQuery()
        {
            return from doctor in _medicalAppointmentContext.Doctors
                   join specialty in _medicalAppointmentContext.Specialties on doctor.SpecialtyID equals specialty.SpecialtyID
                   where doctor.IsActive
                   orderby doctor.CreatedAt descending
                   select new DoctorSpecialtyAvailabilityModel
                   {
                      DoctorID = doctor.DoctorID,
                      SpecialtyID = specialty.SpecialtyID,
                      SpecialtyName = specialty.SpecialtyName,
                      Bio = doctor.Bio,
                      ConsultationFee = doctor.ConsultationFee,
                      ClinicAddress = doctor.ClinicAddress,
                      CreatedAt = doctor.CreatedAt,
                      UpdatedAt = doctor.UpdatedAt,
                      IsActive = doctor.IsActive
                   };
        }

        public async Task<OperationResult> GetDoctorById(int doctorId)
        {
            
                OperationResult operationResult = new OperationResult();
                try
                {
                    var doctor = await this._medicalAppointmentContext.Doctors.FindAsync(doctorId);

                    if (doctor is null)
                    {
                        operationResult.Success = false;
                        operationResult.Message = "El usuario no se encuentra registrado.";
                        return operationResult;
                    }
                    operationResult.Data = doctor;
                }
                catch (Exception ex)
                {
                    operationResult.Message = "Ocurrio un error obteniendo el doctor.";
                    operationResult.Success = false;
                    this._logger.LogError(operationResult.Message, ex.ToString());
                }
                return operationResult;
            
        }
    }
    }
