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
    public class DoctorRepository: BaseRepository<Doctor>, IDoctorRepository
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private readonly ILogger<DoctorRepository> _logger;

        public DoctorRepository(MedicalAppointmentContext medicalAppointmentContext, ILogger<DoctorRepository> logger)
        : base(medicalAppointmentContext)
        {
            _medicalAppointmentContext = medicalAppointmentContext;
            _logger = logger;
        }

        private OperationResult ValidateDoctorEntity(Doctor entity)
        {
            OperationResult operationResult = new OperationResult();
            if (entity == null)
            {
                operationResult.Success = false;
                operationResult.Message = "La entidad es requerida.";
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

            if (!operationResult.Success)
            {
                return operationResult;
            }

            if (await base.Exists(doctor => doctor.LicenseNumber == entity.LicenseNumber))
            {
                return new OperationResult
                {
                    Success = false,
                    Message = "Este doctor ya se encuentra registrado."
                };

            }
            return await ExecuteOperationWithLogging(() => base.Save(entity), "Error guardando el doctor.");
        }
        public async override Task<OperationResult> Update(Doctor entity)
        {
            OperationResult operationResult = ValidateDoctorEntity(entity);

            if (!operationResult.Success)
            {
                return operationResult;
            }

            return await ExecuteOperationWithLogging(async () =>
            {
                Doctor? doctorToUpdate = await _medicalAppointmentContext.Doctors.FindAsync(entity.DoctorID);
                if (doctorToUpdate == null)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Este doctor no existe."
                    };
                }
                doctorToUpdate.SpecialtyID = entity.SpecialtyID;
                doctorToUpdate.LicenseNumber = entity.LicenseNumber;
                doctorToUpdate.YearsOfExperience = entity.YearsOfExperience;
                doctorToUpdate.Bio = entity.Bio;
                doctorToUpdate.ConsultationFee = entity.ConsultationFee;
                doctorToUpdate.ClinicAddress = entity.ClinicAddress;
                doctorToUpdate.AvailabilityModelId = entity.AvailabilityModelId;
                doctorToUpdate.LicenseExpirationDate = entity.LicenseExpirationDate;

                return await base.Update(doctorToUpdate);
            }, "Error actualizando el doctor.");
        }
        public async override Task<OperationResult> Remove(Doctor entity)
        {
            OperationResult operationResult = ValidateDoctorEntity(entity);

            if (!operationResult.Success) { return operationResult; }

            return await ExecuteOperationWithLogging(async () =>
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
                doctorToRemove.UpdatedAt = entity.UpdatedAt;

                return await base.Update(doctorToRemove);
            }, "Error desactivando el doctor.");
        }
        public async override Task<OperationResult> GetAll()
        {
            return await ExecuteOperationWithLogging(async () =>
            {
                var doctorsWithSpecialtyAndAvailabilityModel = await GetDoctorWithSpecialtyAndAvailabilityQuery().ToListAsync();

                return new OperationResult
                {
                    Success = true,
                    Data = doctorsWithSpecialtyAndAvailabilityModel
                };
            }, "Error obteniendo los doctores.");

        }
        public async override Task<OperationResult> GetEntityBy(int Id)
        {
            return await ExecuteOperationWithLogging(async () =>
            {
                var doctorsWithSpecialtyAndAvailabilityModel = await GetDoctorWithSpecialtyAndAvailabilityQuery()
                                       .FirstOrDefaultAsync(doctor => doctor.DoctorID == Id);

                return new OperationResult
                {
                    Success = true,
                    Data = doctorsWithSpecialtyAndAvailabilityModel
                };
            }, "Error obteniendo el doctor.");
        }


        public async Task<OperationResult> GetDoctorBySpecialty(int specialtyId)
        {
            return await ExecuteOperationWithLogging(async () =>
            {
                var doctorsBySpecialty = await GetDoctorWithSpecialtyAndAvailabilityQuery()
                    .Where(doctor => doctor.SpecialtyID == specialtyId)
                    .ToListAsync();

                return new OperationResult
                {
                    Success = true,
                    Data = doctorsBySpecialty
                };
            }, $"Error obteniendo doctores con la especialidad {specialtyId}.");
        }
        public async Task<OperationResult> GetDoctorByHour(DateTime date, TimeSpan hour)
        {
            return await ExecuteOperationWithLogging(async () =>
            {
                var doctorsByHour = await (from doctor in _medicalAppointmentContext.Doctors
                                           join availability in _medicalAppointmentContext.DoctorAvailability
                                           on doctor.DoctorID equals availability.DoctorID
                                           where availability.AvaileDate.Date == date.Date &&
                                                 availability.StartTime.TimeOfDay <= hour &&
                                                 availability.EndTime.TimeOfDay >= hour &&
                                                 doctor.IsActive
                                           select new DoctorSpecialtyAvailabilityAndMode
                                           {
                                               DoctorID = doctor.DoctorID,
                                               SpecialtyID = doctor.SpecialtyID,
                                               LicenseNumber = doctor.LicenseNumber,
                                               YearsOfExperience = doctor.YearsOfExperience,
                                               Bio = doctor.Bio,
                                               ConsultationFee = doctor.ConsultationFee,
                                               ClinicAddress = doctor.ClinicAddress,
                                               AvailabilityModelId = doctor.AvailabilityModelId,
                                               CreatedAt = doctor.CreatedAt,
                                               UpdatedAt = doctor.UpdatedAt,
                                               IsActive = doctor.IsActive,
                                               AvaileDate = availability.AvaileDate,
                                               StartTime = availability.StartTime,
                                               EndTime = availability.EndTime
                                           }).ToListAsync();

                return new OperationResult
                {
                    Success = true,
                    Data = doctorsByHour
                };
            }, $"Error obteniendo doctores disponibles en la fecha {date} y hora {hour}.");
        }
        private IQueryable<DoctorSpecialtyAvailabilityModel> GetDoctorWithSpecialtyAndAvailabilityQuery()
        {
            return from doctor in _medicalAppointmentContext.Doctors
                   join specialty in _medicalAppointmentContext.Specialties on doctor.SpecialtyID equals specialty.SpecialtyID
                   join availabiityMode in _medicalAppointmentContext.AvailabilityModes on doctor.AvailabilityModelId equals availabiityMode.SAvailabilityModeID
                   where doctor.IsActive
                   orderby doctor.CreatedAt descending
                   select new DoctorSpecialtyAvailabilityModel
                   {
                      DoctorID = doctor.DoctorID,
                      SpecialtyID = specialty.SpecialtyID,
                      SpecialtyName = specialty.SpecialtyName,
                      LicenseNumber = doctor.LicenseNumber,
                      YearsOfExperience = doctor.YearsOfExperience,
                      Bio = doctor.Bio,
                      ConsultationFee = doctor.ConsultationFee,
                      ClinicAddress = doctor.ClinicAddress,
                      AvailabilityModelId = doctor.AvailabilityModelId,
                      AvailabilityModelName = availabiityMode.Availability_Mode,
                      LicenseExpirationDate = doctor.LicenseExpirationDate,
                      CreatedAt = doctor.CreatedAt,
                      UpdatedAt = doctor.UpdatedAt,
                      IsActive = doctor.IsActive
                   };
        }
    }
    }
